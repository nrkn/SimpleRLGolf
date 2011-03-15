//alias Console, we use it enough that this ends up being an overall saving
using C = System.Console;
//class containing Main can be called anything
class S {
  //p = position
  //v is counter in first loop and temporary position
  //x and y are used to get the offset into the string holding 1 corner of the map
  //t is an alias for 10 as 10 gets used often enough to warrant aliasing it
  static int p, v, x, y, t = 10;

  //draw the passed in char at the current player location
  //use a default value for '@' to save a couple of bytes
  //as we call D with '@' twice
  //because p is a number 0 to 99 we can get the x and y
  //from it using mod and integer division
  static void D( char c = '@' ) {
    C.SetCursorPosition( p % t, p / t );
    C.Write( c );
  }

  //doesn't need to be public and doesn't need args
  static void Main() {
    var s = "";

    //we have 100 map squares
    for( ; v < 100; v++ )
      //draw the result of the following expression
      D( 
        ( 
        //add a character from the following string to s  
        s += 
            "#### #  ###    ##    #   "[ 
              //the position in the string to add to s...
              //we get x and y by transforming the value (v) 0-99 into a x or y coord
              //then we see if it's outside the corner 5x5 grid that the string above represents,
              //and we wrap it back to a coordinate that does exist
              ( ( y = v / t ) < 5 ? y : 5 - y + 4 ) * 5 + ( ( x = v % t ) < 5 ? x : 5 - x + 4 ) 
            ] 
        )
        //now that we've added the current character to s, we get the index of v in s and set p to it
        //so that the drawing function knows what it is
        [ p = v ] 
      );

    //set p back to the player starting position 2,2
    p = 22;

    //use the pre and post expressions to draw an @ before the loop starts and at the end of each loop
    for( D(); ; D() ) {
      //the 1 > 0 in the readkey stops readkey echoing back the key pressed, it's shorter than the bool literal true
      //we deduct 37 from the key pressed and get an int from 0-3 depending on which arrow key was pressed
      //we've set up an array where that number corresponds to a number with which to modify the position
      //we set v to the position modified by the array member referred to by the keypress
      //this means that any other key will throw an exception, effecting exiting the game
      v = p + new[] { -1, -t, 1, t }[ (int) C.ReadKey( 1 > 0 ).Key - 37 ];
      //draw a blank over the tile the player is currently on
      D( ' ' );
      //update the position if it's possible to move there
      p = s[ v ] == ' ' ? v : p;
    }
  }
}