```
Author:     H. James de St. Germain
Partner:    None
Date:       6-Feb-2022
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  ashhwang
Repo:       https://github.com/Utah-School-of-Computing-de-St-Germain/spreadsheet-ashhwang
Commit #:   f3cf65f42a44a0d365007381ce9aa431d9dc84ef
Project:    FormulaTests
Copyright:  CS 3500 and Jiawen Wang - This work may not be copied for use in Academic Coursework.
```

# Comments to Evaluators:
My FormulaTests project stands on its own.

//Since I am assigned to check "Parenthesis/Operator Following Rule - Any token that immediately
//follows an opening parenthesis or an operator must be either a number, a variable,
//or an opening parenthesis", I need to check whether the following token is a variable
//or not in Formula function.
//
//And to check whether a token is a variable, I'm checking whether the token meets the minimum
//format of a variable, which is, "any letter or underscore followed by any number of letters and/or
//digits and/or underscores would form valid variable names." After checking, I added variable that
//meets minimum format to valid_tokens list.
//
//In API of Evaluate function, I'm asked to throw FormulaError "if no undefined variables".
//But since I'm looping valid_tokens list, there is no "undefined variable" in it, I therefore
//did not return FormulaError about "undefined variable" in Evaluate function.
//
//Hence, I'm only testing FormulaError of division by zero in this test class.

I did not notice that we have a code coverage grading in gradescope, I made some changes to improve the code coverage, could you please accept my work before the final due time on Monday?
# Assignment Specific Topics
None.

# Consulted Peers:
None.

# References:
Microsoft document for methods in c#.
