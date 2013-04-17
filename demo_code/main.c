/*
 * file name : main.c
 * function  : control the main flow
 * date      : 2013-04-16
 * author    : enyblock
 */


#include "common.h"

int main (void)
{
/*
    DATE my_date = {16,04,2013};

    my_date.year = 2014;
    printf ("hello sap\n%d\n",my_date.year);
    printf ("year = %-5d\nmonth = %-5d\nday = %-5d\n",my_date.year,my_date.month,my_date.day);
*/
	int i = 0;
	int j = 0;
	clock_t start_time = clock();
	clock_t end_time = clock();


	while (i < 1500){
		j = 0;
		while (j < 50){
			strcmp(bus_info[i][j].bp_name,"gaoxiang");
			j++;
		}
		i++;
	}

	end_time = clock();


	printf("time = %lf\n",(double)(end_time-start_time)*1000/CLOCKS_PER_SEC);
	


	printf("bus_point = %d\n",sizeof(BUS_POINT));



    return EXIT_SUCCESS;
}
