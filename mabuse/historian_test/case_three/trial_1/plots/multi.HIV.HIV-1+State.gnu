# HIV
# HIV:1+State=HIV_Uninfected_State
# HIV:1+State=HIV_Acute_State
# HIV:1+State=HIV_Chronic_State
# HIV:1+State=HIV_Fatality_State
set terminal postscript eps size 7.0,5.0 enhanced color;
set output 'multi.HIV.HIV-1+State.eps';
set title "Progression of HIV-1 natural history in population" font "Helvetica,20";
set yrange [0.0:1.0];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#FFFF0000' lt 1 lw 1 pt 2 ps 1.0 ;
set style line 2 lc rgb '#FFFFAA00' lt 1 lw 1 pt 3 ps 1.0 ;
set style line 3 lc rgb '#AAAA55FF' lt 1 lw 1 pt 4 ps 1.0 ;
set style line 4 lc rgb '#555500FF' lt 1 lw 1 pt 5 ps 1.0 ;
plot "multi.HIV.HIV-1+State.dat" u ($1/365):2 with linespoints ls 1 t " uninfected ", "multi.HIV.HIV-1+State.dat" u ($1/365):3 with linespoints ls 2 t " acute ", "multi.HIV.HIV-1+State.dat" u ($1/365):4 with linespoints ls 3 t " chronic ", "multi.HIV.HIV-1+State.dat" u ($1/365):5 with linespoints ls 4 t " mortality "
