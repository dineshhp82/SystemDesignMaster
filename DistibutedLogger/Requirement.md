 Design System for Distributed logger with following features.

	- Correlation Id
	- Senstitive data masking
	- Thread Safe
	- Log Rolling
	- Batch Processing 
	- Backpressure handling



+-------------------+
|   ILoggerStrategy |<<interface>>
|-------------------|
| + GetLogFile()    |
+-------------------+
       ^
       |
+------------------------+      +------------------------+
| SizeBasedRolling       |      | TimeBasedRolling       |
|------------------------|      |------------------------|
| + GetLogFile()         |      | + GetLogFile()         |
+------------------------+      +------------------------+

+-----------------+
|   Logger        | (Singleton)
|-----------------|
| - strategy      |
| - lockObj       |
|-----------------|
| + LogInfo(msg)  |
| + LogWarn(msg)  |
| + LogError(msg) |
+-----------------+
