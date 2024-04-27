# HDR-3-DIRS-BUC02  
![image](https://github.com/vintagefilmography/HDR-3-DIRS-BUC02/assets/48537944/74dfee1c-0abb-4279-8e45-93932d777b2a)

This is a Windows app that interfaces with the Hawkeye camera and
produces a sequence of bracketed images that can be postprocessed in
HDR utility.
It produces images with 3 exposures. The first image has exp = normal Second image has exp = normal + (increment)
and the third image has exp = normal - incremenet. The images are stored in 
separate directories. The directories can be combined in several ways to generate the 2 exp or 3 exp videos.  
alternatively, the directory with not exposure change can be used to create a regular non HDR video.  

This High Definition HDR app was made specifically to work with the Imagingsource BUC02 cameras.  
The Hawkeye MSP430 firmware was also modified to provide the 3 exposure selection.
The selection is done by the REWIND switch in slow speed mode.  
The REWIND switch is normally used to rewind film with the RUN switch turned off.  
With the RUN switch ON, the REWIND switch has no affect normally, but in this version it  
is used to switch to HDR mode.
REWIND OFF - 1EXP mode
REWIND ON - 3EXP mode
Important note: When stopping the Hawkeye turn the RUN switch off before the REWIND switch. Failing  
to do that may result in the last dark image missing.

Note: Hawkeye board V12 or higher and MSP FW are required for proper HDR operation.  
The firmware is available here. Download the workspace_v9.zip file and extract it somewhere in the local drive.  
Then in Code Composer use File->Switch Workspace to load in this new wokspace. The project that you want to build is
hdr_3exp

Run instructions:
Check the enclosed manual for the instructions.
