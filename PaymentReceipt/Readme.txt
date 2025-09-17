Payment Receipt
====================================================
                 PAYMENT RECEIPT
====================================================
Receipt ID     : RCP-2025-0001
Issued At      : 2025-09-17 10:32 AM (UTC+05:30)

---------------- PAYER DETAILS --------------------
Name           : Dinesh Sharma
Email          : dinesh.sharma@example.com
Time Zone      : Asia/Kolkata

---------------- PAYMENT ITEMS --------------------
#   Description         Ref ID     Qty   Unit Price   Total
-----------------------------------------------------------
1   Product A           PRD-001    2     500.00 INR   1000.00
2   Product B           PRD-002    1     750.00 INR    750.00
3   Service X           SVC-100    1    1250.00 INR   1250.00
-----------------------------------------------------------
                          SUBTOTAL :           3000.00
                          TAX (10%):            300.00
-----------------------------------------------------------
                          GRAND TOTAL:        3300.00 INR

---------------- PAYMENT METHOD -------------------
Method         : Credit Card
Card Number    : **** **** **** 4321
Transaction ID : TXN-778899

---------------- RECEIPT STATUS -------------------
Status         : PAID

====================================================
   Thank you for your payment!
====================================================





A retail platform needs a Payment Receipt subsystem to:
 - Record receipts when payments succeed (credit card, wallet, bank transfer).
 - Each receipt must be immutable, auditable, and carry currency/amount, payer details, payment method, timestamp, and line items referencing invoices/orders.

 - Receipts must ensure invariants:
		Amount >= 0
 - Currency consistency across lines (or explicit conversion)
 - Receipt must reference at least one PaymentItem (invoice or order)

Provide APIs to:
	Create a receipt (from a successful payment)
	Query receipt by id
	Export a receipt PDF (out of scope for code — show hook)

The system should be safe in concurrent environments and suitable for caching / event publishing.

Key Decision

Receipt = Aggregate Root
 - Immutable after creation
 - Contains PaymentItems (value objects)
 - Enforces invariants at creation

 PaymentItem as Value Object vs Entity: treat as value object if not tracked independently; 
 make it entity if you must reference items elsewhere. We'll treat it as Value Object (belongs to receipt).

- Money value object (amount + currency) with operator helpers.
- Immutable Receipt: once created it should be append-only for audit; updates only via explicit domain operations (e.g., MarkRefunded).
- Domain Event ReceiptCreated published after creation.

- Service layer for orchestration (payment gateway result -> create receipt -> persist -> publish event).


Value Object

- Money  -> Amount + Currency with helper methods.
- PaymentItem -> references invoice/order, amount, Type(Invoice).
- Payer -> Name, Email, TimeZone.

Aggregate Root

- Receipt
  - ReciptId
  - IssueDate
  - Payer
  - Items : PaymentItem[]
  - TotalAmount : Money	
  - Payment Method : enum

  + Create()
  + CalculateTotal() :Money
  + MarkRefunded()	

  Client -> Payment Gateway -> PaymentService -> ReceiptFactory -> ReceiptAggregate -> IReceiptRepository.Save -> EventBus.Publish(ReceiptCreated) -> Response


  +---------------------+        uses         +----------------------+
|   PaymentService    |-------------------->| IReceiptRepository   |
+---------------------+                     +----------------------+
| +CreateReceipt(...) |                     | +Save(receipt)       |
+---------------------+                     | +GetById(id)         |
                                            +----------------------+

                 +-----------------------------+
                 |          Receipt            |  <<Aggregate Root>>
                 +-----------------------------+
                 | - ReceiptId : ReceiptId     |
                 | - IssuedAt : DateTimeOffset |
                 | - Payer : Payer             |
                 | - Items : List<PaymentItem> |
                 | - Total : Money            |
                 | - PaymentMethod : enum     |
                 +-----------------------------+
                 | +Create(...) : Receipt     |
                 | +CalculateTotal() : Money |
                 | +MarkRefunded(...)        |
                 +-----------------------------+

  +--------------+       +---------------+      +----------------+
  |   Payer      |       |  PaymentItem  |      |     Money      |
  | (Value Obj)  |       |  (Value Obj)  |      |  (Value Obj)   |
  +--------------+       +---------------+      +----------------+
  | Name         |       | ReferenceId   |      | Amount : dec   |
  | Email        |       | Type (Invoice)|      | Currency : str |
  | TimeZoneId   |       | Amount :Money |      +----------------+
  +--------------+       +---------------+






  ? Deconstruct
  ? SRP break in receipt creation (validation, total calc, event publish)  
  ? why we need event publish?

