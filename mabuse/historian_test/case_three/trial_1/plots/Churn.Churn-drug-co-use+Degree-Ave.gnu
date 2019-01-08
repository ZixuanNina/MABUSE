# Churn Churn:drug_co_use+Degree|Ave
set terminal postscript eps size 7.0,5.0 enhanced color font 'Helvetica,20' linewidth 2;
set output 'Churn.Churn-drug-co-use+Degree-Ave.eps';
set title "Actual network degree in the Drug co-Use Risk Layer" font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#FFFF0000' lt 1 lw 1 pt 2 ps 1.0 ;

plot "Churn.Churn-drug-co-use+Degree-Ave.dat" u ($1/365):2 with linespoints ls 1 t "Average  ;
