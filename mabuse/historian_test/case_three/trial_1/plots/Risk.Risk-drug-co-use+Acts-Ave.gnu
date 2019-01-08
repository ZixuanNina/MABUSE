# Risk Risk:drug_co_use+Acts|Ave
set terminal postscript eps size 7.0,5.0 enhanced color font 'Helvetica,20' linewidth 2;
set output 'Risk.Risk-drug-co-use+Acts-Ave.eps';
set title "Individual risk acts in the Drug co-Use network layer" font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#FFFF0000' lt 1 lw 1 pt 2 ps 1.0 ;

plot "Risk.Risk-drug-co-use+Acts-Ave.dat" u ($1/365):2 with linespoints ls 1 t "Average  ;
