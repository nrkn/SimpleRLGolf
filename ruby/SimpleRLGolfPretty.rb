# Load up the curses library.  Using #include allows us to use the module's
# methods without the Curses:: prefix.  We also do some basic curses init.
require 'curses'
include Curses
noecho
curs_set 0

# b is the game board in a one-dimensional array.  It is created by taking a
# constant hex value and converting that into its binary representation in
# string form.  Then every zero in the string is replaced with a blank space
# and every 1 is replaced with a hash.
b = 0xf3e798070340902c0e019e7cf.to_s(2).tr '01',' #'

# We create a curses screen and draw the game board to it, by splitting the
# b string into chunks of 10 characters and joining them together again with
# a newline character.
s = init_screen << b.scan(/.{10}/)*$/

# We enable the use of the keypad for the newly-created screen.
s.keypad 1

# We set the initial player position to {2,2}, counting from the top-left.
p=22

# This lambda draws a character at the Player's current location, which is
# represented by a single integer (p).  You can think of the tens digit
# representing y-position while the ones digit represents x-position.
#
# The character to be drawn defaults to an '@', but another can be specified
# by either a single-character string or an ascii code.
#
# The method returns 6 because it is used as an argument to the modulus call
# in the main game loop.
d = -> t=?@ {
  setpos p/10, p%10
  addch t
  6
}

# Draw the player to his initial starting position.
d[]

# Main game loop:
#  * Draw a space (ascii 32) over the current player's position using d[32].
#  * Get a keypress from curses as an integer keycode.
#  * Take that value mod 6 (so KEY_DOWN is , KEY_UP is 1, etc) and use the result
#    as an index into an array of movement options.
#  * Use this movement option to find out the desired target position.  Call this v.
#  * If v is accessible (if it is less than '#', so not a # character) update p to it.
#  * Draw the player in whatever position p points to now.
loop {
  p = b[v=p+[10,-10,-1,1][getch%d[32]]] < ?# ? v : p
  d[]
} 