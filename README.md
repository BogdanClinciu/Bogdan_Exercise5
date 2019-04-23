# Bogdan_Exercise_5-Sudoku

# Requirement
Sudoku Builder and Solver

1. Create an application that builds a 9x9 sudoku board. The resulting board must be solvable. Remove numbers from the board until 15 numbers remain on the board. Save all the generated boards.
2. Create an application to solve the generated boards from the above mentioned application. The user should be able to select the desired board from a dropdown list and click on “Resolve”.

# Quick setup:
1. Download and unzip BUILDS.zip file from the repository root.
2. Run Sudoku_Generator_Bogdan shortcut.
3. Click "Generate" and then "Save" as many times as desired.
4. Run Sudoku_Solver_Bogdan shortcut, select a puzzle from the Dropdown at the top right and click "Solve"

Notes: The database on the solver program only updates after relaunching, so if you generate new puzzles with the generator be sure to exit and restart the solver program.

# Project structure:
The project loosely follows MVC design philosophy with PuzzlueUI.cs handling user input and user interface updates, while PuzzleManager handles the required data sets and data operations.

# Saving, loading and Data types:
- The chosen data type for saving is string, with separator characters to delimit cells and cell rows/columns. If necessary conversion to csv file should be relatively straight forward.
- Saving and loading is accomplished using the built in JsonUtility.

# Solver/Generator logic
A recursive method is used for both puzzle generation and solving, execution times have been measured to be between 0-3 ms for both solving and generating.
- Puzzle generation first fills the top left and bottom right quadrant in a random order to ensure a random grid, secondly it solves the puzzle and finally clears cell data until the specified 15 filled cells remain.
- Puzzle solving uses the same method as in step two of generation using a generated puzzle.
