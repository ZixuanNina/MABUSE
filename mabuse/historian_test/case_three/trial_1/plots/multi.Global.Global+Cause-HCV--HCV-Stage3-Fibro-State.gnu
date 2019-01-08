# Global
# Global+Cause_HCV__HCV_Stage3_Fibro_State|Ave
# Global+Cause_HCV__HCV_Stage3_Fibro_State|Std
set terminal postscript eps size 7.0,5.0 enhanced color;
set output 'multi.Global.Global+Cause-HCV--HCV-Stage3-Fibro-State.eps';
set title "Value of Cause\_HCV\_\_HCV\_Stage3\_Fibro\_State over time" font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#FFFF0000' lt 1 lw 1 pt 2 ps 1.0 ;
set style line 2 lc rgb '#FFFFAA00' lt 1 lw 1 pt 3 ps 1.0 ;
plot "multi.Global.Global+Cause-HCV--HCV-Stage3-Fibro-State.dat" u ($1/365):2 with linespoints ls 1 t "Ave", 