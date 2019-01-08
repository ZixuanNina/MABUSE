# Churn
# Churn:drug_co_use+Degree|Ave
# Churn:drug_co_use+Degree|Std
set terminal postscript eps size 7.0,5.0 enhanced color;
set output 'multi.Churn.Churn-drug-co-use+Degree.eps';
set title "Actual network degree in the Drug co-Use Risk Layer" font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#FFFF0000' lt 1 lw 1 pt 2 ps 1.0 ;
set style line 2 lc rgb '#FFFFAA00' lt 1 lw 1 pt 3 ps 1.0 ;
plot "multi.Churn.Churn-drug-co-use+Degree.dat" u ($1/365):2 with linespoints ls 1 t "Average ", 
