# Place Place:Population+Frequency_Moved|Ave
set terminal postscript eps size 7.0,5.0 enhanced color font 'Helvetica,20' linewidth 2;
set output 'Place.Place-Population+Frequency-Moved-Ave.eps';
set title "Frequency of movement between places " font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#5555FF55' lt 1 lw 1 pt 2 ps 1.0 ;

plot "Place.Place-Population+Frequency-Moved-Ave.dat" u ($1/365):2 with linespoints ls 1 t "Average  ;
