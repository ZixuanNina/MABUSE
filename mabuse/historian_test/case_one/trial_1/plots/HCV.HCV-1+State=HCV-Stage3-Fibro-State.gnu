# HCV HCV:1+State=HCV_Stage3_Fibro_State
set terminal postscript eps size 7.0,5.0 enhanced color font 'Helvetica,20' linewidth 2;
set output 'HCV.HCV-1+State=HCV-Stage3-Fibro-State.eps';
set title "Percentage of population with stage 3 fibrosis due to HCV " font "Helvetica,20";
set yrange [0.0:1.0];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#5555FF55' lt 1 lw 1 pt 2 ps 1.0 ;

plot "HCV.HCV-1+State=HCV-Stage3-Fibro-State.dat" u ($1/365):2 with linespoints ls 1 t " stage 3 fibrosis  ;
