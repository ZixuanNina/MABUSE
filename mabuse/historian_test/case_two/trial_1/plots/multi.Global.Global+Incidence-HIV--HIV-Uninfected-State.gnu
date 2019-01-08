# Global
# Global+Incidence_HIV__HIV_Uninfected_State|Ave
# Global+Incidence_HIV__HIV_Uninfected_State|Std
set terminal postscript eps size 7.0,5.0 enhanced color;
set output 'multi.Global.Global+Incidence-HIV--HIV-Uninfected-State.eps';
set title "Value of Incidence\_HIV\_\_HIV\_Uninfected\_State over time" font "Helvetica,20";
set yrange [*:*];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#5555FF55' lt 1 lw 1 pt 2 ps 1.0 ;
set style line 2 lc rgb '#5555AA00' lt 1 lw 1 pt 3 ps 1.0 ;
plot "multi.Global.Global+Incidence-HIV--HIV-Uninfected-State.dat" u ($1/365):2 with linespoints ls 1 t "Ave", 
