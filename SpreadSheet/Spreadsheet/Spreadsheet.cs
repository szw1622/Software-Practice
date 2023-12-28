/// <summary> 
/// Author:    Jiawen Wang
/// Partner:   None
/// Date:      02/18/2022
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Jiawen Wang - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Jiawen Wang, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// This file can save Spreadsheet and read Spreadsheet. It also accepts cell value information now, compare
/// with AS4. It is working more and more like a real Spreadsheet.
/// </summary>
/// 

using SpreadsheetUtilities;
using System.Text.RegularExpressions;
using System.Xml;

namespace SS
{
    /// <summary>
    /// An AbstractSpreadsheet object represents the state of a simple spreadsheet.  A 
    /// spreadsheet consists of an infinite number of named cells.
    /// 
    /// A string is a valid cell name if and only if:
    ///   (1) its first character is an underscore or a letter
    ///   (2) its remaining characters (if any) are underscores and/or letters and/or digits
    /// Note that this is the same as the definition of valid variable from the PS3 Formula class.
    /// 
    /// For example, "x", "_", "x2", "y_15", and "___" are all valid cell  names, but
    /// "25", "2x", and "&" are not.  Cell names are case sensitive, so "x" and "X" are
    /// different cell names.
    /// 
    /// A spreadsheet contains a cell corresponding to every possible cell name.  (This
    /// means that a spreadsheet contains an infinite number of cells.)  In addition to 
    /// a name, each cell has a contents and a value.  The distinction is important.
    /// 
    /// The contents of a cell can be (1) a string, (2) a double, or (3) a Formula.  If the
    /// contents is an empty string, we say that the cell is empty.  (By analogy, the contents
    /// of a cell in Excel is what is displayed on the editing line when the cell is selected.)
    /// 
    /// In a new spreadsheet, the contents of every cell is the empty string.
    ///  
    /// The value of a cell can be (1) a string, (2) a double, or (3) a FormulaError.  
    /// (By analogy, the value of an Excel cell is what is displayed in that cell's position
    /// in the grid.)
    /// 
    /// If a cell's contents is a string, its value is that string.
    /// 
    /// If a cell's contents is a double, its value is that double.
    /// 
    /// If a cell's contents is a Formula, its value is either a double or a FormulaError,
    /// as reported by the Evaluate method of the Formula class.  The value of a Formula,
    /// of course, can depend on the values of variables.  The value of a variable is the 
    /// value of the spreadsheet cell it names (if that cell's value is a double) or 
    /// is undefined (otherwise).
    /// 
    /// Spreadsheets are never allowed to contain a combination of Formulas that establish
    /// a circular dependency.  A circular dependency exists when a cell depends on itself.
    /// For example, suppose that A1 contains B1*2, B1 contains C1*2, and C1 contains A1*2.
    /// A1 depends on B1, which depends on C1, which depends on A1.  That's a circular
    /// dependency.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        //Stores named cells in Dictionary with cell's name be key, cell's content(for PS4) be value
        private Dictionary<string, Cell> cells_in_sheet;

        //DependencyGraph that keeps track of named cells
        private DependencyGraph dg;

        /// <summary>
        /// This constructor takes 0 argument, creates an empty spreadsheet 
        /// with no extra validity conditions, normalizes every cell name to itself, 
        /// and use the name "default" as the version.
        /// </summary>
        public Spreadsheet() : this(s => true, s => s, "default")
        {
            cells_in_sheet = new();
            dg = new();
            Changed = false;
        }


        /// <summary>
        /// Constructs an spreadsheet by recording its variable validity test,
        /// its normalization method, and its version information.  
        /// </summary>
        /// 
        /// <remarks>
        /// The variable validity test is used throughout to determine whether a string that consists of 
        /// one or more letters followed by one or more digits is a valid cell name.  The variable
        /// equality test should be used throughout to determine whether two variables are equal.
        /// </remarks>
        /// 
        /// <param name="isValid">   defines what valid variables look like for the application</param>
        /// <param name="normalize"> defines a normalization procedure to be applied to all valid variable strings</param>
        /// <param name="version">   defines the version of the spreadsheet (should it be saved)</param>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version) : base(isValid, normalize, version)
        {
            cells_in_sheet = new();
            dg = new();
            Changed = false;
        }

        /// <summary>
        /// Constructs a spreadsheet by recording the path of the file it needs to read,
        /// its variable validity test, its normalization method, and its version information.  
        /// </summary>
        /// <param name="filepath"> path of a file that needs to be read in this spreadsheet</param>
        /// <param name="isValid"> defines what valid variables look like for the application</param>
        /// <param name="normalize"> defines a normalization procedure to be applied to all valid variable strings</param>
        /// <param name="version"> defines the version of the spreadsheet (should it be saved)</param>
        /// <exception cref="SpreadsheetReadWriteException"> thrown when something went wrong in opening, reading or writing file</exception>
        public Spreadsheet(string filepath, Func<string, bool> isValid, Func<string, string> normalize, string version) : this(isValid, normalize, version)
        {
            cells_in_sheet = new();
            dg = new();

            try
            {
                using (XmlReader reader = XmlReader.Create(filepath))
                {
                    if (!GetSavedVersion(filepath).Equals(version))
                    {
                        throw new SpreadsheetReadWriteException("The version of the file being read does not match input version");
                    }
                    else
                    {
                        //reading nodes with elementstring name of "cell" and "content"
                        //Reference: https://www.csharp-examples.net/xml-nodes-by-name/#:~:text=To%20get%20all%20nodes,xmlNode%5B%22FirstName%22%5D.
                        XmlDocument xml = new XmlDocument();
                        xml.Load(filepath);
                        if (xml.SelectSingleNode("//name") == null || xml.SelectSingleNode("//cell") == null || xml.SelectSingleNode("//spreadsheet") == null)
                        {
                            throw new SpreadsheetReadWriteException("No cell's information found in xml file");
                        }
                        XmlNodeList nodeList = xml.SelectNodes("/spreadsheet/cell");

                        foreach (XmlNode node in nodeList)
                        {
                            //if xml file has node "cell", which is, has all cells' information, keep reading information
                            //otherwise, throw SpreadsheetReadWriteException
                            if (node.Name.Equals("cell"))
                            {
                                //if cell's name and content not found in xml file, throw SpreadsheetReadWriteException
                                //otherwise read cell's name and content to spreadsheet
                                if (!node.FirstChild.Name.Equals("name") || !node.LastChild.Name.Equals("contents"))
                                {
                                    throw new SpreadsheetReadWriteException("Cell's name and content not found in xml file");
                                }
                                else
                                {
                                    string a = node["name"].InnerText;
                                    string b = node["contents"].InnerText.ToString();
                                    SetContentsOfCell(a, b);
                                }
                            }
                            else
                            {
                                throw new SpreadsheetReadWriteException("No cell's information found in xml file");
                            }
                        }
                    }

                }
            }
            catch(Exception e)
            {
                throw new SpreadsheetReadWriteException(e.Message);
            }
            Changed = false;
        }

        /// <summary>
        /// When testing, will return true if spreadsheet is ever cahnged, otherwise false
        /// If assigning same contents to same cell, that does not count changed
        /// </summary>
        public override bool Changed { get; protected set; }


        /// <summary>
        /// Helper method to check whether cell name (the onde that after being normalized) 
        /// is valid, which is, whether its normalized version matches pattern 
        /// "one or more letters followed by one or more digits (numbers)"
        /// </summary>
        /// <param name="name"> cell name</param>
        /// <returns>true if cell name is valid</returns>
        private bool nameIsValid(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z]+[0-9]+$") && IsValid(name);
        }

        /// <summary>
        /// Thrown InvalidNameException if the name is invalid: blank/empty/""
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        /// <param name="name">named cell's name</param>
        /// <returns>named cell's content if it has, or empty string if it does not have content</returns>
        /// <exception cref="InvalidNameException">when name of named cell is invalid</exception>
        public override object GetCellContents(string name)
        {
            string normalized_name = Normalize(name);
            if (!nameIsValid(normalized_name))
            {
                throw new InvalidNameException();
            }
            else if (cells_in_sheet.ContainsKey(normalized_name))
            {
                return cells_in_sheet[normalized_name].cell_content;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Thrown InvalidNameException if the name is invalid: blank/empty/""
        /// 
        /// Returns the value (as opposed to the contents) of the named cell.  The return
        /// value should be either a string, a double, or a SpreadsheetUtilities.FormulaError.
        /// </summary>
        /// <param name="name">named cell's name(will be normalized)</param>
        /// <returns> named cell's value if it has, or empty string if it does not have value</returns>
        /// <exception cref="InvalidNameException">when name of named cell is invalid</exception>
        public override object GetCellValue(string name)
        {
            string normalized_name = Normalize(name);
            if (!nameIsValid(normalized_name))
            {
                throw new InvalidNameException();
            }
            else if (cells_in_sheet.ContainsKey(normalized_name))
            {
                object cellVal = cells_in_sheet[normalized_name].cell_value;

                return cellVal;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// If all cells are empty then an IEnumerable with zero values will be returned.
        /// </summary>
        /// <returns>all non empty cells' names</returns>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            foreach (KeyValuePair<string, Cell> cell in cells_in_sheet)
            {
                if (!cell.Value.cell_content.Equals(""))
                {
                    yield return cell.Key;
                }
            }
        }

        /// <summary>
        ///  Look up the version information in the given file. If there are any problems opening, reading, 
        ///  or closing the file, the method should throw a SpreadsheetReadWriteException with an explanatory message.
        ///   
        /// In an ideal world, this method would be marked static as it does not rely on an existing SpreadSheet
        /// object to work; indeed it should simply open a file, lookup the version, and return it.  Because
        /// C# does not support this syntax, we abused the system and simply create a "regular" method to
        /// be implemented by the base class.
        /// 
        /// Throw SpreadsheetReadWriteException if any problem occurs while reading the file or looking up the version information.
        /// </summary>
        /// <param name="filename">name of the file that we want to knwo the version of </param>
        /// <returns> the version of input file</returns>
        /// <exception cref="SpreadsheetReadWriteException"> thrown if any problem occurs while reading the file or looking up the version information.</exception>
        public override string GetSavedVersion(string filename)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    string get_saved_version="";
                    reader.MoveToContent();
                    //reader.ReadStartElement("spreadsheet");
                    if (reader.MoveToAttribute("version"))
                    {
                        get_saved_version += reader.Value;

                    }
                    else
                    {
                        throw new SpreadsheetReadWriteException("No version information in the file");
                    }
                    return get_saved_version;
                }
            }
            catch(Exception e)
            {
                throw new SpreadsheetReadWriteException(e.Message);
            }
        }

        /// <summary>
        /// Writes the contents of this spreadsheet to the named file using an XML format.
        /// The XML elements should be structured as follows:
        /// 
        /// <spreadsheet version="version information goes here">
        /// 
        /// <cell>
        /// <name>cell name goes here</name>
        /// <contents>cell contents goes here</contents>    
        /// </cell>
        /// 
        /// </spreadsheet>
        /// 
        /// There should be one cell element for each non-empty cell in the spreadsheet.  
        /// If the cell contains a string, it should be written as the contents.  
        /// If the cell contains a double d, d.ToString() should be written as the contents.  
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        /// 
        /// If there are any problems opening, writing, or closing the file, the method should throw a
        /// SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        /// <param name="filename">name of file</param>
        /// <exception cref="SpreadsheetReadWriteException"> thrown when something went wrong in opening, reading or writing file</exception>
        public override void Save(string filename)
        {
            if(filename is null)
            {
                throw new SpreadsheetReadWriteException("file name is null");
            }

            XmlWriterSettings settings = new();
            settings.Indent = true;
            settings.IndentChars = " ";
            try
            {
                using (XmlWriter writer = XmlWriter.Create(filename,settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("spreadsheet");
                    writer.WriteAttributeString("version", Version); //write spreadsheet version
                    foreach(string name in GetNamesOfAllNonemptyCells())
                    {
                        writer.WriteStartElement("cell");
                        writer.WriteElementString("name", name); //write cell name
                        if(cells_in_sheet[name].cell_content is Formula)
                        {
                            writer.WriteElementString("contents", "="+cells_in_sheet[name].cell_content.ToString());
                        }
                        else
                        {
                            writer.WriteElementString("contents", cells_in_sheet[name].cell_content.ToString()); //write cell contents
                        }
                        
                        writer.WriteEndElement(); //write end element "/cell"
                    }
                    writer.WriteEndElement(); //write end element "/spreadsheet"
                    Changed = false; //set Changed to false since a xml file just created here
                                     //chekc documentation of Changed
                }
            }
            catch (Exception e)
            {
                throw new SpreadsheetReadWriteException(e.Message);
            }

        }

        /// <summary>
        /// Set the contents of the named cell to the given number.  
        /// 
        /// The name parameter must be valid: non-empty/not ""
        /// If the name is invalid, throw an InvalidNameException
        /// 
        /// This method returns a LIST consisting of the passed in name followed by the names of all 
        /// other cells whose value depends, directly or indirectly, on the named cell.
        /// 
        /// The order must correspond to a valid dependency ordering for recomputing
        /// all of the cells, i.e., if you re-evaluate each cell in the order of the list,
        /// the overall spreadsheet will be consistently updated.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned, i.e., A1 was changed, so then A1 must be 
        /// evaluated, followed by B1 re-evaluated, followed by C1 re-evaluated.
        /// </summary>
        /// <param name="normalized_name">name of named cell</param>
        /// <param name="number">double content that named cell is set to</param>
        /// <returns>named cell and other cells that depend on it</returns>
        /// <exception cref="InvalidNameException">when name of named cell is invalid</exception>
        protected override IList<string> SetCellContents(string name, double number)
        {
            string normalized_name = Normalize(name);
            if (!nameIsValid(normalized_name))
            {
                throw new InvalidNameException();
            }
            if (cells_in_sheet.ContainsKey(normalized_name))
            {
                cells_in_sheet[normalized_name].cell_content = number; //if have named cell, reset its content to number
                cells_in_sheet[normalized_name].cell_value = number;
            }
            else
            {
                Cell new_cell = new(number);
                cells_in_sheet.Add(normalized_name, new_cell); //if not have named cell, add named cell with number as content
            }

            HashSet<string> empty_set = new();
            dg.ReplaceDependees(normalized_name, empty_set); //add named cell to dependency graph

            //return list consists of named cell and other cells that depend on it
            return GetCellsToRecalculate(normalized_name).ToList();
        }

        /// <summary>
        /// The contents of the named cell becomes the text.  
        /// 
        /// The name parameter must be valid/non-empty ""
        /// If the name is invalid, throw an InvalidNameException
        /// 
        /// This method returns a LIST consisting of the passed in name followed by the names of all 
        /// other cells whose value depends, directly or indirectly, on the named cell.
        /// 
        /// The order must correspond to a valid dependency ordering for recomputing
        /// all of the cells, i.e., if you re-evaluate each cell in the order of the list,
        /// the overall spreadsheet will be consistently updated.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned, i.e., A1 was changed, so then A1 must be 
        /// evaluated, followed by B1 re-evaluated, followed by C1 re-evaluated.
        /// </summary>
        /// <param name="normalized_name">name of named cell</param>
        /// <param name="text">string content that named cell is set to</param>
        /// <returns>named cell and other cells that depend on it</returns>
        /// <exception cref="InvalidNameException">/// <exception cref="InvalidNameException">when name of named cell is null or invalid</exception>
        protected override IList<string> SetCellContents(string name, string text)
        {

            string normalized_name = Normalize(name);

            if (!nameIsValid(normalized_name))
            {
                throw new InvalidNameException();
            }

            if (cells_in_sheet.ContainsKey(normalized_name))
            {
                cells_in_sheet[normalized_name].cell_content = text; //if have named cell, reset its content to text
                cells_in_sheet[normalized_name].cell_value = text;
            }
            else
            {
                Cell new_cell = new(text);
                cells_in_sheet.Add(normalized_name,new_cell); //if not have named cell, add named cell with text as content
            }

            HashSet<string> empty_set = new();
            dg.ReplaceDependees(normalized_name, empty_set); //add named cell to dependency graph

            //return list consists of named cell and other cells that depend on it
            return GetCellsToRecalculate(normalized_name).ToList();
        }

        /// <summary>
        /// If the formula parameter is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException, and no change is made to the spreadsheet.
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// list consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// list {A1, B1, C1} is returned.
        /// </summary>
        /// <param name="name">name of named cell</param>
        /// <param name="formula">formula content that named cell is set to</param>
        /// <returns>named cell and other cells that depend on it</returns>
        /// <exception cref="InvalidNameException">when name of named cell is null or invalid</exception>
        protected override IList<string> SetCellContents(string name, Formula formula)
        {
            string normalized_name = Normalize(name);

            if (!nameIsValid(normalized_name))
            {
                throw new InvalidNameException();
            }

            //store old dependees of input named cell
            IEnumerable<string> old_dependees = dg.GetDependees(normalized_name);
            List<string> olds = new List<string>();
            foreach(string t in old_dependees)
            {
                olds.Add(t);
            }

            //change dependees of input named cell, no matter if SetCellContent(name)
            //causes Circular Dependency or not
            dg.ReplaceDependees(normalized_name, formula.GetVariables());

            try
            {
                //check if CircularException happens if setting input named cell's 
                //content to input formula
                IList<string> changed_cells = GetCellsToRecalculate(normalized_name).ToList();

                Cell new_cell = new Cell(formula, spreadsheet_lookup);
                if (cells_in_sheet.ContainsKey(normalized_name))
                {
                    cells_in_sheet[normalized_name] = new_cell; //if have named cell, update cell's information

                }
                else
                {
                    cells_in_sheet.Add(normalized_name, new_cell); //if not have named cell, add named cell with Formula as content
                }

                return changed_cells;
            }
            catch (CircularException) //if CircularException being catched, do the following
            {
                //if Circular Dependency happened, reset Dependency Graph to the version
                //before setting input named cell's content to the input formula

                dg.ReplaceDependees(normalized_name, olds);

                throw new CircularException();
            }

        }

        /// <summary>
        ///   <para>Sets the contents of the named cell to the appropriate value. </para>
        ///   <para>
        ///       First, if the content parses as a double, the contents of the named
        ///       cell becomes that double.
        ///   </para>
        ///
        ///   <para>
        ///       Otherwise, if content begins with the character '=', an attempt is made
        ///       to parse the remainder of content into a Formula.  
        ///       There are then three possible outcomes:
        ///   </para>
        ///
        ///   <list type="number">
        ///       <item>
        ///           If the remainder of content cannot be parsed into a Formula, a 
        ///           SpreadsheetUtilities.FormulaFormatException is thrown.
        ///       </item>
        /// 
        ///       <item>
        ///           If changing the contents of the named cell to be f
        ///           would cause a circular dependency, a CircularException is thrown,
        ///           and no change is made to the spreadsheet.
        ///       </item>
        ///
        ///       <item>
        ///           Otherwise, the contents of the named cell becomes f.
        ///       </item>
        ///   </list>
        ///
        ///   <para>
        ///       Finally, if the content is a string that is not a double and does not
        ///       begin with an "=" (equal sign), save the content as a string.
        ///   </para>
        /// </summary>
        ///
        /// <exception cref="InvalidNameException"> 
        ///   If the name parameter is null or invalid, throw an InvalidNameException
        /// </exception>
        /// 
        /// <exception cref="SpreadsheetUtilities.FormulaFormatException"> 
        ///   If the content is "=XYZ" where XYZ is an invalid formula, throw a FormulaFormatException.
        /// </exception>
        /// 
        /// <exception cref="CircularException"> 
        ///   If changing the contents of the named cell to be the formula would 
        ///   cause a circular dependency, throw a CircularException.  
        ///   (NOTE: No change is made to the spreadsheet.)
        /// </exception>
        /// 
        /// <param name="name"> The cell name that is being changed</param>
        /// <param name="content"> The new content of the cell</param>
        /// 
        /// <returns>
        ///       <para>
        ///           This method returns a list consisting of the passed in cell name,
        ///           followed by the names of all other cells whose value depends, directly
        ///           or indirectly, on the named cell. The order of the list MUST BE any
        ///           order such that if cells are re-evaluated in that order, their dependencies 
        ///           are satisfied by the time they are evaluated.
        ///       </para>
        ///
        ///       <para>
        ///           For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        ///           list {A1, B1, C1} is returned.  If the cells are then evaluate din the order:
        ///           A1, then B1, then C1, the integrity of the Spreadsheet is maintained.
        ///       </para>
        /// </returns>
        public override IList<string> SetContentsOfCell(string name, string content)
        {
            string normalized_name = Normalize(name);

            if (normalized_name is null || !nameIsValid(normalized_name))
            {
                throw new InvalidNameException();
            }

            //create new list to store cell that changes content and other cells that depend on that cell
            IList<string> updated_cells = new List<string>();

            //check when sheet contains named cell and cell content equals to input content,
            //mark spreadsheet unchanged, which is, update Changed to false
            //else if cell content does not equals to input content,
            //mark spreadsheet changed, which is, update Changed to true, and reset content
            if (double.TryParse(content, out double double_content)) //chekc if input content is of type double
            {
                if (cells_in_sheet.ContainsKey(normalized_name) && cells_in_sheet[normalized_name].cell_content.Equals(double_content))
                {
                    Changed = false;
                }
                else
                {
                    Changed = true;
                    
                }
                updated_cells = SetCellContents(normalized_name, double_content);
            }
            //Need to separate this case with non-empty string content
            //Because I check if content is a string by checking if the first char of
            //string is not '='. But for an empty string, I cannot access the first char,
            //because it will cause IndexOutOfRangeException
            else if (content.Equals("")) 
            {
                if (cells_in_sheet.ContainsKey(normalized_name) && cells_in_sheet[normalized_name].cell_content.Equals(content))
                {
                    Changed = false;
                }
                else
                {
                    Changed = true;
                    
                }
                updated_cells = SetCellContents(normalized_name, content);
            }
            else if(content.Substring(0, 1).Equals("="))//check if input content could be a formula
            {
                try
                {
                    Formula f = new(content.Substring(1,content.Length-1), Normalize, IsValid); //check if input formula content could be parsed into a formula
                                                                  //by parsing Normalize and IsValid set in Spreadsheet 
                                                                  //see more in Formula constructor in Formula.cs
                    if (cells_in_sheet.ContainsKey(normalized_name) && f.Equals(cells_in_sheet[normalized_name].cell_content))
                    {
                        Changed = false;
                    }
                    else
                    {
                        Changed = true;
                        //updated_cells = SetCellContents(normalized_name, f);
                    }
                    updated_cells = SetCellContents(normalized_name, f);

                }
                catch (FormulaFormatException) //handle the case that content cannot be parsed into a formula
                {
                    throw new SpreadsheetUtilities.FormulaFormatException("Input content cannot be parsed into a formula");
                }
                catch (CircularException) //handle circular dependency caused in seting input content
                {
                    throw new CircularException();
                }
                
            }
            else //case where input content is string, and is not empty
            {
                if (cells_in_sheet.ContainsKey(normalized_name) && cells_in_sheet[normalized_name].cell_content.Equals(content))
                {
                    Changed = false;
                }
                else
                {
                    Changed = true;
                    //updated_cells = SetCellContents(normalized_name, content);
                }
                updated_cells = SetCellContents(normalized_name, content);
            }

            foreach (string s in updated_cells)
            {
                //Cell cell_value;
                // try to get the key, if we find it, re-evaluate it
                if (cells_in_sheet.TryGetValue(s, out Cell cell))
                {
                    if(cell.cell_content is Formula)
                    {
                        Formula f = (Formula)cell.cell_content;
                        cell.cell_value = f.Evaluate(spreadsheet_lookup);
                    }

                }

            }
            return updated_cells;
        }


        /// <summary>
        /// Returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        /// <param name="name">named ceel's name</param>
        /// <returns>dependents of named cell</returns>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            return dg.GetDependents(name);
        }

        /// <summary>
        /// Helper method, is spreadsheet version of lookup function
        /// cell's name is also variabl's name
        /// if cell not assigned value in sheet, throw ArgumentException
        /// if cell's value cause FormulaError, throw ArgumentException
        /// </summary>
        /// <param name="name">cell name</param>
        /// <returns> cell's value </returns>
        /// <exception cref="ArgumentException"> thrown if cant find cell's value</exception>
        private double spreadsheet_lookup(string name) //name is the cell's name in spreadsheet, also be variable of formula
        {

            if (cells_in_sheet.ContainsKey(name))
            {
                if (cells_in_sheet[name].cell_value is double)
                {
                    return (double)cells_in_sheet[name].cell_value;
                }
                else if (cells_in_sheet[name].cell_value is FormulaError)
                {
                    throw new ArgumentException("formulaerror happened");
                }
                else
                {
                    throw new ArgumentException("Variable value is not double, not allowed");
                }
            }
            else
            {
                throw new ArgumentException("Cannot find variable in spreasheet");
            }
        }




        /// <summary>
        /// A class that stores 
        /// </summary>
        private class Cell
        {

            //Stores cell's content
            public object cell_content;

            //Stores cell's value, not yet used in PS4
            public object cell_value;

            /// <summary>
            /// Constructor of cell
            /// Set cell's content of double type
            /// Set cell's value of string type
            /// </summary>
            /// <param name="content">content of named cell</param>
            public Cell(double content)
            {
                cell_content = content;
                cell_value = content;
            }

            /// <summary>
            /// Constructor of cell
            /// Set cell's content of string type
            /// Set cell's value of string type
            /// </summary>
            /// <param name="content">content of named cell</param>
            public Cell(string content)
            {
                cell_content = content;
                cell_value = content;
            }

            /// <summary>
            /// Constructor of cell
            /// Set cell's content of Formula type
            /// 
            /// If a cell's contents is a Formula, its value is either a double or a FormulaError,
            /// as reported by the Evaluate method of the Formula class.  The value of a Formula,
            /// of course, can depend on the values of variables.  The value of a variable is the 
            /// value of the spreadsheet cell it names (if that cell's value is a double) or 
            /// is undefined (otherwise).
            /// 
            /// 
            /// </summary>
            /// <param name="content">content of named cell</param>
            public Cell(Formula content, Func<string, double> lookup)
            {
                cell_content = content;
                cell_value = content.Evaluate(lookup);

            }

        }

    }
}