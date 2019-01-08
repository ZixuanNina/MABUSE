# Global Global+Cause_Aging__AliveState_NaturalDeath|Std
set terminal postscript eps size 7.0,5.0 enhanced color font 'Helvetica,20' linewidth 2;
set output 'Global.Global+Cause-Aging--AliveState-NaturalDeath-Std.eps';
set title "Value of Cause\_Aging\_\_AliveState\_NaturalDeath over time" font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#FFFF0000' lt 1 lw 1 pt 2 ps 1.0 ;

plot "Global.Global+Cause-Aging--AliveState-NaturalDeath-Std.dat" u ($1/365):2 with linespoints ls 1 t "Std ;
