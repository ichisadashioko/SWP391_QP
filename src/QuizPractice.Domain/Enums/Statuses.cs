namespace QuizPractice.Domain.Enums;

// Status values of BR-23. The database stores the name of each member, with a
// space between words ("In Progress"), and a CHECK constraint keeps it to this list.

/// <summary>Setting, PricePackage, Lesson, Quiz.</summary>
public enum ActiveStatus { Active, Inactive }

/// <summary>Post, Slider, Question.</summary>
public enum VisibilityStatus { Shown, Hidden }

/// <summary>User (D22): Unverified until the email link is used.</summary>
public enum UserStatus { Unverified, Active, Inactive }

/// <summary>Subject: only the Admin switches it (BR-12).</summary>
public enum SubjectStatus { Unpublished, Published }

/// <summary>Registration (BR-22): only Submitted moves; Paid and Cancelled are final.</summary>
public enum RegistrationStatus { Submitted, Paid, Cancelled }

/// <summary>Invoice (BR-24).</summary>
public enum InvoiceStatus { Unpaid, Paid, Cancelled }

/// <summary>Attempt: never goes back.</summary>
public enum AttemptStatus { InProgress, Submitted }
