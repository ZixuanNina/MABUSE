# Place
# Place+Population=default_place
set terminal postscript eps size 7.0,5.0 enhanced color;
set output 'multi.Place.Place+Population.eps';
set title "The Total Population " font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#5555FF55' lt 1 lw 1 pt 2 ps 1.0 ;
plot "multi.Place.Place+Population.dat" u ($1/365):2 with linespoints ls 1 t "default_place"
