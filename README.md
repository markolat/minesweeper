# Minesweeper game

*Developed using C#, WPF Application*

## Instructions for MineSweeper

**Quick Start:**

You are presented with a board of squares. Some squares contain mines (bombs), others don't. If you click on a square containing a bomb, you lose. If you manage to click all the squares (without clicking on any bombs) you win.
Clicking a square which doesn't have a bomb reveals the number of neighbouring squares containing bombs. Use this information plus some guess work to avoid the bombs.
To open a square, point at the square and click on it. To mark a square you think is a bomb, point and right-click (or hover with the mouse and press Space).

**Detailed Instructions:**

A squares "neighbours" are the squares adjacent above, below, left, right, and all 4 diagonals. Squares on the sides of the board or in a corner have fewer neighbors. The board does not wrap around the edges.
If you open a square with 0 neighboring bombs, all its neighbors will automatically open. This can cause a large area to automatically open.
To place a marker (flag) on a square, that helps you keep track of squares that have bombs, point at it and right-click it.
To remove a bomb marker from a square, point at it and right-click again.
The first square you open is never a bomb.
If you mark a bomb incorrectly, you will have to correct the mistake before you can win. Incorrect bomb marking doesn't kill you, but it can lead to mistakes which do.
You don't have to mark all the bombs to win; you just need to open all non-bomb squares.
Click the yellow smiley button, on top of window, to start a new game.

**Status Information:**

The upper left corner contains a field that shows the number of bombs left to find. The number will update as you mark and unmark squares.
The upper right corner contains a field that represents time counter. The timer will max out at 999 (16 minutes 39 seconds).
