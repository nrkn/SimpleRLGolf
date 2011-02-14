//alias Console, we use it enough that this ends up being an overall saving
using C = System.Console;
//class containing Main can be called anything
class S {
  //only x and y used outside main loop but it's cheaper to init all at once
  //k = last key value
  //u & v = temporary storage for x & y
  //x & y = player location
  int k, u, v, x, y;

  //if we put the code in Main we'd have to make local members static
  //it's cheaper to have the constructor and new it up
  S() {
    //we only need the top left part of the map
    //method W derives the other 4 corners of the map from this
    //this string is 5 rows of 5 columns run together
    var m = "#### #  ###    ##    #   ";

    //draw the map
    //don't need to init y, it's already 0
    for( ; y < 10; y++ )
      //but need to init x each time through
      for( x = 0; x < 10; x++ )
        //draw map at x, y
        D( m[ W( y ) * 5 + W( x ) ] );

    //initial player location
    x = y = 2;
    
    //draw @ for first time
    D();
    
    //cheapest infinite loop (well, weighs the same as a goto :P)
    for( ; ; ) {
      //subtract 36 from the int value of the keypress
      //it's cheaper to test against single digit numbers
      k = (int) C.ReadKey( true ).Key - 36;

      //make a copy of x and y
      u = x;
      v = y;
      
      //blank out the player location
      D( ' ' );

      //test the key entered and modify u or v accordingly
      //we only assign back to k to trick the compiler into
      //letting us use a ternary instead of an if/else because
      //even with the extra 'k =' we save several bytes that way
      //read the ternary like this:
      //if k % 2 then up or down was pressed
      //  if k is 2 then up, minus 1 from v 
      //  else must be down (4), add 1 to v
      //else must be left or right
      //  if k is 1 then left, minus 1 from u
      //  else right, add 1 to u     
      //
      //this actually lets you enter input with all sorts of keys 
      //other than arrows but so long as q quits and arrows move
      //doesn't matter
      k = k % 2 < 1 ? v += k < 3 ? -1 : 1 : u += k < 2 ? -1 : 1;

      //if map is empty at u, v then set x, y to u, v
      if( m[ W( v ) * 5 + W( u ) ] == ' ' ) {
        x = u;
        y = v;
      }
      
      //draw the @
      D();
    }
  }

  //doesn't need to be public and doesn't need args
  static void Main() {
    //run the game!
    new S();
  }

  //draw the passed in char at the current player location
  //use a default value for '@' to save a couple of bytes
  //as we call D with '@' twice
  void D( char c = '@' ) {
    C.SetCursorPosition( x, y );
    C.Write( c );
  }

  //we only store once corner of the map, if we want a location
  //that's out of bounds this uses a mirror copy of that corner
  int W( int i ) {
    return i < 5 ? i : 5 - i + 4;
  }
}