# Global Global+Incidence_HCV__HCV_Stage3_Fibro_State|Ave
set terminal postscript eps size 7.0,5.0 enhanced color font 'Helvetica,20' linewidth 2;
set output 'Global.Global+Incidence-HCV--HCV-Stage3-Fibro-State-Ave.eps';
set title "Value of Incidence\_HCV\_\_HCV\_Stage3\_Fibro\_State over time" font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#5555FF55' lt 1 lw 1 pt 2 ps 1.0 ;

plot "Global.Global+Incidence-HCV--HCV-Stage3-Fibro-State-Ave.dat" u ($1/365):2 with linespoints ls 1 t "Ave ;
