/*
 * file name : main.c
 * function  : control the main flow
 * date      : 2013-04-16
 * author    : enyblock
 */

#include <stdio.h>
#include <stdlib.h>
#include "common.h"
int main (void)
{

    DATE my_date = {16,04,2013};

    my_date.year = 2014;
    printf ("hello sap\n%d\n",my_date.year);
    printf ("year = %-5d\nmonth = %-5d\nday = %-5d\n",my_date.year,my_date.month,my_date.day);

    return EXIT_SUCCESS;
}
