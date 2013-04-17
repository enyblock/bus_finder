/*
 * file name  : common.h
 * function   : declare all data  including bus point
 * date       : 2013-04-16
 * author     : enyblock
 */



#ifndef _ENYBLOCK_2013_04_16_SAP_XIAN_LAB_
#define _ENYBLOCK_2013_04_16_SAP_XIAN_LAB_


#include <time.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>



typedef struct  _date {
   int day;
   int month;
   int year;
} DATE;

#define BUS_LINE_NAME_LEN           50
#define BUS_POINT_NAME_LEN          100
#define BUS_LINE_DESCRIPTION_LEN    200
#define BUS_LINE_DESCRIPTION_TYPE   10


/*define the bus line data type*/
typedef struct _bus_line{

   unsigned int bl_index;                           /*define the bus line's index*/
   unsigned int bl_type;                            /*define the bus line's type: up or down*/
   unsigned int bl_point_num;                       /*define the number of the point*/
   char         bl_name[BUS_LINE_NAME_LEN];         /*define the bus line's name*/
                                                    /*define the bus line's description*/
   char         bl_description[BUS_LINE_DESCRIPTION_TYPE][BUS_LINE_DESCRIPTION_LEN];

} BUS_LINE;




/*define the point data type*/
typedef struct _bus_point{

   unsigned int bl_index;                      /*define the bus line's index*/
   unsigned int bl_type;                       /*define the bus line's type: up or down*/
   unsigned int bp_index;                      /*define the bus point's index*/
   char         bp_name[BUS_POINT_NAME_LEN];   /*define the bus point's name*/

} BUS_POINT;


BUS_POINT bus_info[1500][50];


#endif
