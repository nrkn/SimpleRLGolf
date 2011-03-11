require 'curses';include Curses
def d t=?@;setpos $y||=2,$x||=2;addch t;end
s=init_screen;noecho;curs_set 0;s.keypad 1
b=0xf3e798070340902c0e019e7cf.to_s(2).tr('01',' #').scan /.{10}/
addstr b*$/;d
while v='5317'[s.getch-258]-?0 rescue exit
x,y=$x+v/3-1,$y+v%3-1
d 32;$x,$y=x,y if b[y][x]==32;d;
end