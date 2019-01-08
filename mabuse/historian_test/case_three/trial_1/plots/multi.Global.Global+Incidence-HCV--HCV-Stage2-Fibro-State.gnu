# Global
# Global+Incidence_HCV__HCV_Stage2_Fibro_State|Ave
# Global+Incidence_HCV__HCV_Stage2_Fibro_State|Std
set terminal postscript eps size 7.0,5.0 enhanced color;
set output 'multi.Global.Global+Incidence-HCV--HCV-Stage2-Fibro-State.eps';
set title "Value of Incidence\_HCV\_\_HCV\_Stage2\_Fibro\_State over time" font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#FFFF0000' lt 1 lw 1 pt 2 ps 1.0 ;
set style line 2 lc rgb '#FFFFAA00' lt 1 lw 1 pt 3 ps 1.0 ;
plot "multi.Global.Global+Incidence-HCV--HCV-Stage2-Fibro-State.dat" u ($1/365):2 with linespoints ls 1 t "Ave", 
