# HCV
# HCV:1+State=HCV_Negative_State
# HCV:1+State=HCV_Acute_State
# HCV:1+State=HCV_Chronic_State
# HCV:1+State=HCV_Stage1_Fibro_State
# HCV:1+State=HCV_Stage2_Fibro_State
# HCV:1+State=HCV_Stage3_Fibro_State
# HCV:1+State=HCV_Cirrhosis_State
# HCV:1+State=HCV_Decomp_State
# HCV:1+State=HCV_HCC_State
# HCV:1+State=HCV_Fatality_State
set terminal postscript eps size 7.0,5.0 enhanced color;
set output 'multi.HCV.HCV-1+State.eps';
set title "" font "Helvetica,20";
set yrange [0.0:1.0];
set xlabel '{/Helvetica-Oblique Time (in Years)}';
set style line 1 lc rgb '#FFFF0000' lt 1 lw 1 pt 2 ps 1.0 ;
set style line 2 lc rgb '#FFFFAA00' lt 1 lw 1 pt 3 ps 1.0 ;
set style line 3 lc rgb '#AAAA55FF' lt 1 lw 1 pt 4 ps 1.0 ;
set style line 4 lc rgb '#555500FF' lt 1 lw 1 pt 5 ps 1.0 ;
set style line 5 lc rgb '#FFFF0055' lt 1 lw 1 pt 6 ps 1.0 ;
set style line 6 lc rgb '#00005555' lt 1 lw 1 pt 7 ps 1.0 ;
set style line 7 lc rgb '#FFFFAAAA' lt 1 lw 1 pt 8 ps 1.0 ;
set style line 8 lc rgb '#555500AA' lt 1 lw 1 pt 9 ps 1.0 ;
set style line 9 lc rgb '#0000AAFF' lt 1 lw 1 pt 10 ps 1.0 ;
set style line 10 lc rgb '#5555AA55' lt 1 lw 1 pt 1 ps 1.0 ;
plot "multi.HCV.HCV-1+State.dat" u ($1/365):2 with linespoints ls 1 t " uninfected ", "multi.HCV.HCV-1+State.dat" u ($1/365):3 with linespoints ls 2 t " acute ", "multi.HCV.HCV-1+State.dat" u ($1/365):4 with linespoints ls 3 t " chronic ", "multi.HCV.HCV-1+State.dat" u ($1/365):5 with linespoints ls 4 t " stage 1 fibrosis ", "multi.HCV.HCV-1+State.dat" u ($1/365):6 with linespoints ls 5 t " stage 2 fibrosis ", "multi.HCV.HCV-1+State.dat" u ($1/365):7 with linespoints ls 6 t " stage 3 fibrosis ", "multi.HCV.HCV-1+State.dat" u ($1/365):8 with linespoints ls 7 t " cirrhosis ", "multi.HCV.HCV-1+State.dat" u ($1/365):9 with linespoints ls 8 t " decompensated ", "multi.HCV.HCV-1+State.dat" u ($1/365):10 with linespoints ls 9 t " HCC ", "multi.HCV.HCV-1+State.dat" u ($1/365):11 with linespoints ls 10 t " mortality "
