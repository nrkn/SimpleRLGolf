# Load up the curses library.  Using #include allows us to use the module's
# methods without the Curses:: prefix.
require 'curses'
include Curses

# This method draws a single character at the current cursor position.  The
# cursor position is represented by a single integer, you can think of the 
# tens digit representing y-position while the ones digit represents x-position.
#
# The character to be drawn defaults to an '@', but another can be specified
# by either a single-character string or an ascii code.
def d t=?@
  setpos $p/10, $p%10
  addch t
end

# Basic curses initialization
s = init_screen
noecho
curs_set 0
s.keypad 1

# b is the game board in a one-dimensional array.  It is created by taking a
# constant hex value and converting that into its binary representation in
# string form.  Then every zero in the string is replaced with a blank space
# and every 1 is replaced with a hash.
b = 0xf3e798070340902c0e019e7cf.to_s(2).tr '01',' #'

# Draw the board to the screen, assume rows of 10 characters.
addstr b.scan(/.{10}/)*$/

# Set the initial player position to {2,2}, counting from the top-left.
$p=22

# Draw the player to the screen.
d

# Main game loop:
#  * Get a keypress from curses as an integer keycode.
#  * Subtract 258 (KEY_DOWN) from it and use the result as an index into an
#    array of movement options
while v=$p+[10,-10,-1,1][s.getch-258]
  # Clear the tile the player is standing on (32 is ascii space)
  d 32
  # If the spot the player wants to move to (v) is clear, update $p
  $p=v if b[v]==' '
  # Redraw the player in the new spot (or old spot if he did not move)
  d
end