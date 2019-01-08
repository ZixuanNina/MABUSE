# Churn
# Churn:drug_co_use+AbsDeviation|Ave
# Churn:drug_co_use+AbsDeviation|Std
set terminal postscript eps size 7.0,5.0 enhanced color;
set output 'multi.Churn.Churn-drug-co-use+AbsDeviation.eps';
set title "|Actual-Ideal| network degree in the Drug co-Use Risk Layer" font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#5555FF55' lt 1 lw 1 pt 2 ps 1.0 ;
set style line 2 lc rgb '#5555AA00' lt 1 lw 1 pt 3 ps 1.0 ;
plot "multi.Churn.Churn-drug-co-use+AbsDeviation.dat" u ($1/365):2 with linespoints ls 1 t "Average ", 
