- Library Management
  - The library management system should allow librarians to manage books, members, and borrowing activities.
  - The system should support adding, updating, and removing books from the library catalog.
  - Each book should have details such as title, author, ISBN, publication year, and availability status.
  - The system should allow members to borrow and return books.
  - Each member should have details such as name, member ID, contact information, and borrowing history.
  - The system should enforce borrowing rules, such as a maximum number of books that can be borrowed at a time and loan duration.
  - The system should handle concurrent access to the library catalog and member records.
  - The system should be extensible to accommodate future enhancements and new features.


  Book ->  title, author, ISBN, publication year, and availability status ->  CURD
  Memeber -> name, member ID, contact information, and borrowing history -> BrrowBook and ReturnBook
  Borrowing Rules -> 
	 - Max books per member at a time
	 - Loan duration (days)
	 - Track borrowing history
  Concurrency
  - Multiple members can access catalog or borrow books simultaneously
  - Ensure thread-safety for updates


  Flow
  Student/Memeber come on system to borrow book
  Check brorrowing rules
   if `can borrow` then
	  Search book by title/author/ISBN
	  if `book available` then
		Borrow book
		Update book status to borrowed
		Update member borrowing history
	  else
		Notify book not available
  else 
    Notify book can  not be borrowed





[Member] -> BorrowBook(bookId)
      |
      v
[LibraryService] --validate--> [Borrowing Rules]
      |
      v
[BookRepository] --update--> Book status: Borrowed
[MemberRepository] --update--> Add borrowing record
      |
      v
[Member] updated



ReturnBook flow is similar:
- Validate member and book
- Update book status to Available
- Update borrowing history