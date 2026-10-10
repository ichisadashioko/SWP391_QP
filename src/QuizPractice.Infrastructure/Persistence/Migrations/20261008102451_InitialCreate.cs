using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizPractice.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    bank_account_id = table.Column<int>(type: "int", nullable: false),
                    bank_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    bank_bin = table.Column<string>(type: "char(6)", unicode: false, fixedLength: true, maxLength: 6, nullable: false),
                    account_number = table.Column<string>(type: "varchar(19)", unicode: false, maxLength: 19, nullable: false),
                    account_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.bank_account_id);
                    table.CheckConstraint("CK_BankAccounts_account_number", "[account_number] NOT LIKE '%[^0-9]%'");
                    table.CheckConstraint("CK_BankAccounts_bank_account_id", "[bank_account_id] = 1");
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    setting_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    display_order = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.setting_id);
                    table.CheckConstraint("CK_Settings_display_order", "[display_order] >= 1");
                    table.CheckConstraint("CK_Settings_status", "[status] IN ('Active', 'Inactive')");
                    table.CheckConstraint("CK_Settings_type", "[type] IN ('User Role', 'System Menu', 'Post Category', 'Subject Category', 'Test Type', 'Question Level', 'Lesson Type', 'Dimension Type')");
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    slider_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    backlink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    display_order = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "Hidden")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.slider_id);
                    table.CheckConstraint("CK_Sliders_backlink", "[backlink] LIKE 'https://%'");
                    table.CheckConstraint("CK_Sliders_display_order", "[display_order] >= 1");
                    table.CheckConstraint("CK_Sliders_status", "[status] IN ('Shown', 'Hidden')");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    gender = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    mobile = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    role = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "Unverified"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                    table.CheckConstraint("CK_Users_full_name", "LEN([full_name]) >= 2");
                    table.CheckConstraint("CK_Users_gender", "[gender] IN ('Male', 'Female', 'Other')");
                    table.CheckConstraint("CK_Users_mobile", "[mobile] LIKE '0[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'");
                    table.CheckConstraint("CK_Users_status", "[status] IN ('Unverified', 'Active', 'Inactive')");
                    table.ForeignKey(
                        name: "FK_Users_Settings_role",
                        column: x => x.role,
                        principalTable: "Settings",
                        principalColumn: "setting_id");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    invoice_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    confirmed_by = table.Column<int>(type: "int", nullable: true),
                    code = table.Column<string>(type: "char(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(12,0)", precision: 12, scale: 0, nullable: false),
                    bank_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    bank_bin = table.Column<string>(type: "char(6)", unicode: false, fixedLength: true, maxLength: 6, nullable: false),
                    account_number = table.Column<string>(type: "varchar(19)", unicode: false, maxLength: 19, nullable: false),
                    account_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "Unpaid"),
                    created_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    paid_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.invoice_id);
                    table.CheckConstraint("CK_Invoices_account_number", "[account_number] NOT LIKE '%[^0-9]%'");
                    table.CheckConstraint("CK_Invoices_status", "[status] IN ('Unpaid', 'Paid', 'Cancelled')");
                    table.CheckConstraint("CK_Invoices_total_amount", "[total_amount] > 0");
                    table.ForeignKey(
                        name: "FK_Invoices_Users_confirmed_by",
                        column: x => x.confirmed_by,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Invoices_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    post_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    author_id = table.Column<int>(type: "int", nullable: false),
                    category = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    thumbnail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    brief = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_featured = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "Hidden"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.post_id);
                    table.CheckConstraint("CK_Posts_status", "[status] IN ('Shown', 'Hidden')");
                    table.ForeignKey(
                        name: "FK_Posts_Settings_category",
                        column: x => x.category,
                        principalTable: "Settings",
                        principalColumn: "setting_id");
                    table.ForeignKey(
                        name: "FK_Posts_Users_author_id",
                        column: x => x.author_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    subject_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    expert_id = table.Column<int>(type: "int", nullable: true),
                    category = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    tagline = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    thumbnail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    brief = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_featured = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    pass_rate = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)50),
                    status = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false, defaultValue: "Unpublished"),
                    updated_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.subject_id);
                    table.CheckConstraint("CK_Subjects_pass_rate", "[pass_rate] BETWEEN 0 AND 100");
                    table.CheckConstraint("CK_Subjects_status", "[status] IN ('Unpublished', 'Published')");
                    table.ForeignKey(
                        name: "FK_Subjects_Settings_category",
                        column: x => x.category,
                        principalTable: "Settings",
                        principalColumn: "setting_id");
                    table.ForeignKey(
                        name: "FK_Subjects_Users_expert_id",
                        column: x => x.expert_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "PricePackages",
                columns: table => new
                {
                    package_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subject_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    access_months = table.Column<byte>(type: "tinyint", nullable: false),
                    list_price = table.Column<decimal>(type: "decimal(12,0)", precision: 12, scale: 0, nullable: false),
                    sale_price = table.Column<decimal>(type: "decimal(12,0)", precision: 12, scale: 0, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricePackages", x => x.package_id);
                    table.CheckConstraint("CK_PricePackages_access_months", "[access_months] BETWEEN 1 AND 24");
                    table.CheckConstraint("CK_PricePackages_list_price", "[list_price] >= 0");
                    table.CheckConstraint("CK_PricePackages_sale_price", "[sale_price] >= 0 AND [sale_price] <= [list_price]");
                    table.CheckConstraint("CK_PricePackages_status", "[status] IN ('Active', 'Inactive')");
                    table.ForeignKey(
                        name: "FK_PricePackages_Subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "Subjects",
                        principalColumn: "subject_id");
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    quiz_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subject_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    level = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    question_count = table.Column<short>(type: "smallint", nullable: false),
                    selection_type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    duration_min = table.Column<short>(type: "smallint", nullable: false),
                    pass_rate = table.Column<byte>(type: "tinyint", nullable: false),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.quiz_id);
                    table.CheckConstraint("CK_Quizzes_duration_min", "[duration_min] BETWEEN 1 AND 180");
                    table.CheckConstraint("CK_Quizzes_pass_rate", "[pass_rate] BETWEEN 0 AND 100");
                    table.CheckConstraint("CK_Quizzes_question_count", "[question_count] BETWEEN 1 AND 100");
                    table.CheckConstraint("CK_Quizzes_status", "[status] IN ('Active', 'Inactive')");
                    table.ForeignKey(
                        name: "FK_Quizzes_Settings_level",
                        column: x => x.level,
                        principalTable: "Settings",
                        principalColumn: "setting_id");
                    table.ForeignKey(
                        name: "FK_Quizzes_Settings_type",
                        column: x => x.type,
                        principalTable: "Settings",
                        principalColumn: "setting_id");
                    table.ForeignKey(
                        name: "FK_Quizzes_Subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "Subjects",
                        principalColumn: "subject_id");
                });

            migrationBuilder.CreateTable(
                name: "SubjectDimensions",
                columns: table => new
                {
                    dimension_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subject_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectDimensions", x => x.dimension_id);
                    table.ForeignKey(
                        name: "FK_SubjectDimensions_Settings_type",
                        column: x => x.type,
                        principalTable: "Settings",
                        principalColumn: "setting_id");
                    table.ForeignKey(
                        name: "FK_SubjectDimensions_Subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "Subjects",
                        principalColumn: "subject_id");
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    registration_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subject_id = table.Column<int>(type: "int", nullable: false),
                    package_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: false),
                    invoice_id = table.Column<int>(type: "int", nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    mobile = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    gender = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    total_cost = table.Column<decimal>(type: "decimal(12,0)", precision: 12, scale: 0, nullable: false),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "Submitted"),
                    registered_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.registration_id);
                    table.CheckConstraint("CK_Registrations_gender", "[gender] IN ('Male', 'Female', 'Other')");
                    table.CheckConstraint("CK_Registrations_status", "[status] IN ('Submitted', 'Paid', 'Cancelled')");
                    table.CheckConstraint("CK_Registrations_total_cost", "[total_cost] >= 0");
                    table.CheckConstraint("CK_Registrations_valid_to", "[valid_to] >= [valid_from]");
                    table.ForeignKey(
                        name: "FK_Registrations_Invoices_invoice_id",
                        column: x => x.invoice_id,
                        principalTable: "Invoices",
                        principalColumn: "invoice_id");
                    table.ForeignKey(
                        name: "FK_Registrations_PricePackages_package_id",
                        column: x => x.package_id,
                        principalTable: "PricePackages",
                        principalColumn: "package_id");
                    table.ForeignKey(
                        name: "FK_Registrations_Subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "Subjects",
                        principalColumn: "subject_id");
                    table.ForeignKey(
                        name: "FK_Registrations_Users_created_by",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Registrations_Users_updated_by",
                        column: x => x.updated_by,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Registrations_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Attempts",
                columns: table => new
                {
                    attempt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    subject_id = table.Column<int>(type: "int", nullable: false),
                    quiz_id = table.Column<int>(type: "int", nullable: true),
                    mode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    selection_type = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    group_id = table.Column<int>(type: "int", nullable: true),
                    question_count = table.Column<short>(type: "smallint", nullable: false),
                    current_order = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    started_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    finished_at = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    score = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    pass_rate = table.Column<byte>(type: "tinyint", nullable: true),
                    status = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false, defaultValue: "In Progress")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attempts", x => x.attempt_id);
                    table.CheckConstraint("CK_Attempts_mode", "[mode] IN ('Practice', 'Exam')");
                    table.CheckConstraint("CK_Attempts_pass_rate", "[pass_rate] BETWEEN 0 AND 100");
                    table.CheckConstraint("CK_Attempts_question_count", "[question_count] BETWEEN 1 AND 100");
                    table.CheckConstraint("CK_Attempts_score", "[score] BETWEEN 0 AND 100");
                    table.CheckConstraint("CK_Attempts_selection_type", "[selection_type] IN ('Topic', 'Dimension')");
                    table.CheckConstraint("CK_Attempts_status", "[status] IN ('In Progress', 'Submitted')");
                    table.ForeignKey(
                        name: "FK_Attempts_Quizzes_quiz_id",
                        column: x => x.quiz_id,
                        principalTable: "Quizzes",
                        principalColumn: "quiz_id");
                    table.ForeignKey(
                        name: "FK_Attempts_Subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "Subjects",
                        principalColumn: "subject_id");
                    table.ForeignKey(
                        name: "FK_Attempts_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    lesson_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subject_id = table.Column<int>(type: "int", nullable: false),
                    quiz_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<int>(type: "int", nullable: false),
                    topic_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    display_order = table.Column<int>(type: "int", nullable: false),
                    video_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    html_content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.lesson_id);
                    table.CheckConstraint("CK_Lessons_display_order", "[display_order] >= 1");
                    table.CheckConstraint("CK_Lessons_status", "[status] IN ('Active', 'Inactive')");
                    table.CheckConstraint("CK_Lessons_video_url", "[video_url] LIKE 'https://%'");
                    table.ForeignKey(
                        name: "FK_Lessons_Lessons_topic_id",
                        column: x => x.topic_id,
                        principalTable: "Lessons",
                        principalColumn: "lesson_id");
                    table.ForeignKey(
                        name: "FK_Lessons_Quizzes_quiz_id",
                        column: x => x.quiz_id,
                        principalTable: "Quizzes",
                        principalColumn: "quiz_id");
                    table.ForeignKey(
                        name: "FK_Lessons_Settings_type",
                        column: x => x.type,
                        principalTable: "Settings",
                        principalColumn: "setting_id");
                    table.ForeignKey(
                        name: "FK_Lessons_Subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "Subjects",
                        principalColumn: "subject_id");
                });

            migrationBuilder.CreateTable(
                name: "QuizRules",
                columns: table => new
                {
                    quiz_id = table.Column<int>(type: "int", nullable: false),
                    group_id = table.Column<int>(type: "int", nullable: false),
                    question_count = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizRules", x => new { x.quiz_id, x.group_id });
                    table.CheckConstraint("CK_QuizRules_question_count", "[question_count] >= 1");
                    table.ForeignKey(
                        name: "FK_QuizRules_Quizzes_quiz_id",
                        column: x => x.quiz_id,
                        principalTable: "Quizzes",
                        principalColumn: "quiz_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    question_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subject_id = table.Column<int>(type: "int", nullable: false),
                    lesson_id = table.Column<int>(type: "int", nullable: true),
                    level = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    media = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "Shown")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.question_id);
                    table.CheckConstraint("CK_Questions_status", "[status] IN ('Shown', 'Hidden')");
                    table.ForeignKey(
                        name: "FK_Questions_Lessons_lesson_id",
                        column: x => x.lesson_id,
                        principalTable: "Lessons",
                        principalColumn: "lesson_id");
                    table.ForeignKey(
                        name: "FK_Questions_Settings_level",
                        column: x => x.level,
                        principalTable: "Settings",
                        principalColumn: "setting_id");
                    table.ForeignKey(
                        name: "FK_Questions_Subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "Subjects",
                        principalColumn: "subject_id");
                });

            migrationBuilder.CreateTable(
                name: "AnswerOptions",
                columns: table => new
                {
                    option_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    is_correct = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    display_order = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerOptions", x => x.option_id);
                    table.CheckConstraint("CK_AnswerOptions_display_order", "[display_order] BETWEEN 1 AND 6");
                    table.ForeignKey(
                        name: "FK_AnswerOptions_Questions_question_id",
                        column: x => x.question_id,
                        principalTable: "Questions",
                        principalColumn: "question_id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionDimensions",
                columns: table => new
                {
                    question_id = table.Column<int>(type: "int", nullable: false),
                    dimension_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionDimensions", x => new { x.question_id, x.dimension_id });
                    table.ForeignKey(
                        name: "FK_QuestionDimensions_Questions_question_id",
                        column: x => x.question_id,
                        principalTable: "Questions",
                        principalColumn: "question_id");
                    table.ForeignKey(
                        name: "FK_QuestionDimensions_SubjectDimensions_dimension_id",
                        column: x => x.dimension_id,
                        principalTable: "SubjectDimensions",
                        principalColumn: "dimension_id");
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    quiz_id = table.Column<int>(type: "int", nullable: false),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    display_order = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => new { x.quiz_id, x.question_id });
                    table.CheckConstraint("CK_QuizQuestions_display_order", "[display_order] >= 1");
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Questions_question_id",
                        column: x => x.question_id,
                        principalTable: "Questions",
                        principalColumn: "question_id");
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Quizzes_quiz_id",
                        column: x => x.quiz_id,
                        principalTable: "Quizzes",
                        principalColumn: "quiz_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttemptQuestions",
                columns: table => new
                {
                    attempt_id = table.Column<int>(type: "int", nullable: false),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    chosen_option_id = table.Column<int>(type: "int", nullable: true),
                    display_order = table.Column<short>(type: "smallint", nullable: false),
                    is_marked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttemptQuestions", x => new { x.attempt_id, x.question_id });
                    table.CheckConstraint("CK_AttemptQuestions_display_order", "[display_order] >= 1");
                    table.ForeignKey(
                        name: "FK_AttemptQuestions_AnswerOptions_chosen_option_id",
                        column: x => x.chosen_option_id,
                        principalTable: "AnswerOptions",
                        principalColumn: "option_id");
                    table.ForeignKey(
                        name: "FK_AttemptQuestions_Attempts_attempt_id",
                        column: x => x.attempt_id,
                        principalTable: "Attempts",
                        principalColumn: "attempt_id");
                    table.ForeignKey(
                        name: "FK_AttemptQuestions_Questions_question_id",
                        column: x => x.question_id,
                        principalTable: "Questions",
                        principalColumn: "question_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptions_question_id",
                table: "AnswerOptions",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "UX_AnswerOptions_question_id_correct",
                table: "AnswerOptions",
                column: "question_id",
                unique: true,
                filter: "[is_correct] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptQuestions_chosen_option_id",
                table: "AttemptQuestions",
                column: "chosen_option_id");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptQuestions_question_id",
                table: "AttemptQuestions",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_Attempts_quiz_id",
                table: "Attempts",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "IX_Attempts_subject_id",
                table: "Attempts",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_Attempts_user_id",
                table: "Attempts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_code",
                table: "Invoices",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_confirmed_by",
                table: "Invoices",
                column: "confirmed_by");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_user_id",
                table: "Invoices",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_quiz_id",
                table: "Lessons",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_subject_id",
                table: "Lessons",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_topic_id",
                table: "Lessons",
                column: "topic_id");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_type",
                table: "Lessons",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_author_id",
                table: "Posts",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_category",
                table: "Posts",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "IX_PricePackages_subject_id",
                table: "PricePackages",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionDimensions_dimension_id",
                table: "QuestionDimensions",
                column: "dimension_id");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_lesson_id",
                table: "Questions",
                column: "lesson_id");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_level",
                table: "Questions",
                column: "level");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_subject_id",
                table: "Questions",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_question_id",
                table: "QuizQuestions",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_level",
                table: "Quizzes",
                column: "level");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_subject_id",
                table: "Quizzes",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_type",
                table: "Quizzes",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_created_by",
                table: "Registrations",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_invoice_id",
                table: "Registrations",
                column: "invoice_id");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_package_id",
                table: "Registrations",
                column: "package_id");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_subject_id",
                table: "Registrations",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_updated_by",
                table: "Registrations",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_user_id",
                table: "Registrations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_type_value",
                table: "Settings",
                columns: new[] { "type", "value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectDimensions_subject_id",
                table: "SubjectDimensions",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectDimensions_type",
                table: "SubjectDimensions",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_category",
                table: "Subjects",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_expert_id",
                table: "Subjects",
                column: "expert_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_email",
                table: "Users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_role",
                table: "Users",
                column: "role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttemptQuestions");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "QuestionDimensions");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "QuizRules");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "AnswerOptions");

            migrationBuilder.DropTable(
                name: "Attempts");

            migrationBuilder.DropTable(
                name: "SubjectDimensions");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "PricePackages");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
