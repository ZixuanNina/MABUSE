# Global Global+Cause_HIV__HIV_Chronic_State|Std
set terminal postscript eps size 7.0,5.0 enhanced color font 'Helvetica,20' linewidth 2;
set output 'Global.Global+Cause-HIV--HIV-Chronic-State-Std.eps';
set title "Value of Cause\_HIV\_\_HIV\_Chronic\_State over time" font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#FFFF0000' lt 1 lw 1 pt 2 ps 1.0 ;

plot "Global.Global+Cause-HIV--HIV-Chronic-State-Std.dat" u ($1/365):2 with linespoints ls 1 t "Std ;
