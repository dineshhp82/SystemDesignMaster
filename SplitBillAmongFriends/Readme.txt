Requirements
   - A Bill has a total amount.
   - A Group of Friends participates in the bill.
   - The bill can be split equally or custom (percent/amount).
   - Each friend owes an amount → generate a SplitResult.
   - Keep it extensible (can add new strategies later, e.g., weighted split).


- Friend  -Id ,Name

- SpiltStrategy - Interface	 Split(Bill bill): List<Share>
      - EqualSplitStrategy - Implements SpiltStrategy
      - CustomSplitStrategy - Implements SpiltStrategy

- Share

     - Friend
     - AmountOwed

 - Bill
    - BillId
    - TotalAmount
    - Friends: Friend[]

- SpiltService
   - SplitBill(Bill bill, SpiltStrategy strategy): List<Share>





   +-------------------+
|   Bill (Entity)   |
|-------------------|
| BillId            |
| TotalAmount       |
| Friends[]         |
+-------------------+

+--------------------+
|   Friend (Entity)  |
|--------------------|
| FriendId           |
| Name               |
+--------------------+

+-----------------------------+
|  SplitStrategy (Interface)  |<--- Strategy Pattern
|-----------------------------|
| Split(Bill) -> List<Share>  |
+-----------------------------+
        /            \
       /              \
+----------------+  +-------------------+
| EqualSplit     |  | CustomSplit       |
|----------------|  |-------------------|
| Split(Bill)    |  | Split(Bill)       |
+----------------+  +-------------------+

+--------------------+
|   Share (VO)       |
|--------------------|
| FriendId           |
| AmountOwed         |
+--------------------+

+--------------------+
| SplitService       |<--- Orchestrator
|--------------------|
| ExecuteSplit()     |
+--------------------+

    
 
