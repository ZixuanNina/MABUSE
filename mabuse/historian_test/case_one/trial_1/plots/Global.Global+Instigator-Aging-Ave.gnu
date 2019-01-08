# Global Global+Instigator_Aging|Ave
set terminal postscript eps size 7.0,5.0 enhanced color font 'Helvetica,20' linewidth 2;
set output 'Global.Global+Instigator-Aging-Ave.eps';
set title "Value of Instigator\_Aging over time" font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#5555FF55' lt 1 lw 1 pt 2 ps 1.0 ;

plot "Global.Global+Instigator-Aging-Ave.dat" u ($1/365):2 with linespoints ls 1 t "Ave ;