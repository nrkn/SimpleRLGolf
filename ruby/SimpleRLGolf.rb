require 'curses';include Curses
def d t=?@;setpos $p/10,$p%10;addch t;end
s=init_screen;noecho;curs_set 0;s.keypad 1
b=0xf3e798070340902c0e019e7cf.to_s(2).tr'01',' #'
addstr b.scan(/.{10}/)*$/;$p=22;d
while v=$p+[10,-10,-1,1][s.getch-258]
d 32;$p=v if b[v]==' ';d
end