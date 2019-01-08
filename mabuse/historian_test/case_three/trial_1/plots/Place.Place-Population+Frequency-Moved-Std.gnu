# Place Place:Population+Frequency_Moved|Std
set terminal postscript eps size 7.0,5.0 enhanced color font 'Helvetica,20' linewidth 2;
set output 'Place.Place-Population+Frequency-Moved-Std.eps';
set title "Frequency of movement between places " font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#FFFF0000' lt 1 lw 1 pt 2 ps 1.0 ;

plot "Place.Place-Population+Frequency-Moved-Std.dat" u ($1/365):2 with linespoints ls 1 t "Std. deviation  ;
