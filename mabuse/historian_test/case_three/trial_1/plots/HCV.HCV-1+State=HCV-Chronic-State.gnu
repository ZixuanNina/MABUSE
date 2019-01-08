# HCV HCV:1+State=HCV_Chronic_State
set terminal postscript eps size 7.0,5.0 enhanced color font 'Helvetica,20' linewidth 2;
set output 'HCV.HCV-1+State=HCV-Chronic-State.eps';
set title "Percentage of population with chronic HCV infection " font "Helvetica,20";
set yrange [0.0:1.0];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#FFFF0000' lt 1 lw 1 pt 2 ps 1.0 ;

plot "HCV.HCV-1+State=HCV-Chronic-State.dat" u ($1/365):2 with linespoints ls 1 t " chronic  ;