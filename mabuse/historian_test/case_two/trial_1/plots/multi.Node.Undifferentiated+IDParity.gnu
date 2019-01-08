# Node
# Undifferentiated+IDParity=Even
# Undifferentiated+IDParity=Odd
set terminal postscript eps size 7.0,5.0 enhanced color;
set output 'multi.Node.Undifferentiated+IDParity.eps';
set title "Distribution of IDParity over time" font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#5555FF55' lt 1 lw 1 pt 2 ps 1.0 ;
set style line 2 lc rgb '#5555AA00' lt 1 lw 1 pt 3 ps 1.0 ;
plot "multi.Node.Undifferentiated+IDParity.dat" u ($1/365):2 with linespoints ls 1 t "Even", "multi.Node.Undifferentiated+IDParity.dat" u ($1/365):3 with linespoints ls 2 t "Odd"
