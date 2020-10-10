CREATE DATABASE HumanManagementDB;

CREATE TABLE DEPARTMENT (	
	department_id INT		IDENTITY(1, 1)		NOT NULL,
	department_name VARCHAR(50)					NOT NULL,
	PRIMARY KEY (department_id)
);

CREATE TABLE GRADELEVEL (	
	gradelevel_id INT		IDENTITY(1, 1)		NOT NULL,
	gradelevel_payRate INT						NOT NULL,
	PRIMARY KEY (gradelevel_id)
);

CREATE TABLE JOB (	
	job_id INT				IDENTITY(1, 1)		NOT NULL,
	job_name VARCHAR(50)						NOT NULL,
	job_gradelv_id INT							NOT NULL,
	PRIMARY KEY (job_id),
	FOREIGN KEY (job_gradelv_id) REFERENCES GRADELEVEL(gradelevel_id)
);

CREATE TABLE SKILL (	
	skill_id INT			IDENTITY(1, 1)		NOT NULL,
	skill_name VARCHAR(100)						NOT NULL,
	PRIMARY KEY (skill_id)
);

CREATE TABLE WORKINGSTATUS (	
	workingstatus_id INT	IDENTITY(1, 1)		NOT NULL,
	workingstatus_name VARCHAR(20)				NOT NULL,
	PRIMARY KEY (workingstatus_id)
);

CREATE TABLE EMPLOYEE (
	employee_id INT			IDENTITY(1, 1)		NOT NULL,
	employee_SSN CHAR(9)						NOT NULL,
	employee_name VARCHAR(100)					NOT NULL,
	employee_dob DATE							NOT NULL,
	employee_sex CHAR(10)						NOT NULL,
	employee_maritalStatus CHAR(10)				NOT NULL,
	employee_image VARCHAR(MAX),
	employee_phone CHAR(10)						NOT NULL,
	employee_address VARCHAR(MAX)				NOT NULL,
	employee_mail VARCHAR(100)					NOT NULL,
	employee_hireDate DATE						NOT NULL,
	employee_superiorName VARCHAR(100),
	employee_balances INT						NOT NULL,
	employee_dept_id INT						NOT NULL,
	employee_gralv_id INT						NOT NULL,
	employee_jb_id INT							NOT NULL,
	employee_workstt_id INT						NOT NULL,
	PRIMARY KEY (employee_id),
	FOREIGN KEY (employee_dept_id) REFERENCES DEPARTMENT(department_id),
	FOREIGN KEY (employee_gralv_id) REFERENCES GRADELEVEL(gradelevel_id),
	FOREIGN KEY (employee_jb_id) REFERENCES JOB(job_id),
	FOREIGN KEY (employee_workstt_id) REFERENCES WORKINGSTATUS(workingstatus_id),
	UNIQUE (employee_SSN)		
);

CREATE TABLE EMP_HAS_SKILL (
	emp_id INT									NOT NULL,	
	skl_id INT									NOT NULL,
	PRIMARY KEY (emp_id, skl_id),
	FOREIGN KEY (emp_id) REFERENCES EMPLOYEE(employee_id),
	FOREIGN KEY (skl_id) REFERENCES SKILL(skill_id)
);

CREATE TABLE REASON_TIMEOFF (	
	reason_timeoff_id INT	IDENTITY(1, 1)		NOT NULL,
	reason_timeoff_name VARCHAR(50)				NOT NULL,
	PRIMARY KEY (reason_timeoff_id)
);

CREATE TABLE TYPE_TIMEOFF (	
	type_timeoff_id INT		IDENTITY(1, 1)		NOT NULL,
	type_timeoff_name VARCHAR(50)				NOT NULL,
	PRIMARY KEY (type_timeoff_id)
);

CREATE TABLE TIMEOFF (	
	timeoff_id INT			IDENTITY(1, 1)		NOT NULL,
	timeoff_date DATE							NOT NULL,
	timeoff_numberOfHours DECIMAL(3,2)			NOT NULL,
	timeoff_res_id INT							NOT NULL,
	timeoff_tpe_id INT							NOT NULL,
	timeoff_comment VARCHAR(MAX),
	timeoff_emp_id INT							NOT NULL,
	PRIMARY KEY (timeoff_id),
	FOREIGN KEY (timeoff_emp_id) REFERENCES EMPLOYEE(employee_id),
	FOREIGN KEY (timeoff_res_id) REFERENCES REASON_TIMEOFF(reason_timeoff_id),
	FOREIGN KEY (timeoff_tpe_id) REFERENCES TYPE_TIMEOFF(type_timeoff_id)
);

CREATE TABLE HOLIDAYSCHEDULE (	
	holidayschedule_id INT	IDENTITY(1, 1)		NOT NULL,
	holidayschedule_date DATE					NOT NULL,
	holidayschedule_name VARCHAR(50)			NOT NULL,
	holidayschedule_hours INT					NOT NULL,
	PRIMARY KEY (holidayschedule_id)
);

CREATE TABLE EMP_WORK_ON_HOLIDAY (
	emp_id INT									NOT NULL,	
	hol_id INT									NOT NULL,
	PRIMARY KEY (emp_id, hol_id),
	FOREIGN KEY (emp_id) REFERENCES EMPLOYEE(employee_id),
	FOREIGN KEY (hol_id) REFERENCES HOLIDAYSCHEDULE(holidayschedule_id)
);

CREATE TABLE WEEKLYSALARY (	
	weeklysalary_id INT		IDENTITY(1, 1)		NOT NULL,
	weeklysalary_startDate DATE					NOT NULL,
	weeklysalary_endDate DATE					NOT NULL,
	weeklysalary_hours INT						NOT NULL,
	weeklysalary_emp_id INT						NOT NULL,
	PRIMARY KEY (weeklysalary_id),
	FOREIGN KEY (weeklysalary_emp_id) REFERENCES EMPLOYEE(employee_id)
);

CREATE TABLE BONUS (	
	bonus_id INT			IDENTITY(1, 1)		NOT NULL,
	bonus_gradelv_id INT						NOT NULL,
	bonus_percent INT							NOT NULL,
	PRIMARY KEY (bonus_id),
	FOREIGN KEY (bonus_gradelv_id) REFERENCES GRADELEVEL(gradelevel_id)
);

CREATE TABLE OVERTIME (		
	overtime_id INT			IDENTITY(1, 1)		NOT NULL,
	overtime_date DATE							NOT NULL,
	overtime_hours DECIMAL(3,2)					NOT NULL,
	overtime_comment VARCHAR(MAX)				NOT NULL,
	overtime_emp_id INT							NOT NULL,
	PRIMARY KEY (overtime_id),
	FOREIGN KEY (overtime_emp_id) REFERENCES EMPLOYEE(employee_id)
);

CREATE TABLE RELATION (
	relation_id INT			IDENTITY(1, 1)		NOT NULL,
	relation_name VARCHAR(20)					NOT NULL,
	PRIMARY KEY (relation_id)				
);

CREATE TABLE STATES (
	state_code CHAR(2)							NOT NULL,
	state_name VARCHAR(50)						NOT NULL,
	PRIMARY KEY (state_code),
	UNIQUE (state_code)			
);

CREATE TABLE BENEFICIARY (		
	beneficiary_id INT		IDENTITY(1, 1)		NOT NULL,
	beneficiary_fName VARCHAR(20)				NOT NULL,
	beneficiary_lName VARCHAR(20)				NOT NULL,
	beneficiary_relationship INT				NOT NULL,
	beneficiary_contact CHAR(10),
	beneficiary_addressLine VARCHAR(MAX)		NOT NULL,
	beneficiary_city VARCHAR(MAX)				NOT NULL,
	beneficiary_state CHAR(2)					NOT NULL,
	beneficiary_zipCode CHAR(5)					NOT NULL,
	beneficiary_country VARCHAR(50)				NOT NULL,
	beneficiary_email VARCHAR(100),
	beneficiary_emp_id INT						NOT NULL,
	PRIMARY KEY (beneficiary_id),
	FOREIGN KEY (beneficiary_emp_id) REFERENCES EMPLOYEE(employee_id),
	FOREIGN KEY (beneficiary_relationship) REFERENCES RELATION(relation_id),
	FOREIGN KEY (beneficiary_state) REFERENCES STATES(state_code)
);

INSERT INTO DEPARTMENT VALUES ('Product Development');
INSERT INTO DEPARTMENT VALUES ('Project Management');
INSERT INTO DEPARTMENT VALUES ('Human Resources');
INSERT INTO DEPARTMENT VALUES ('Marketing');
INSERT INTO DEPARTMENT VALUES ('Sales');
INSERT INTO DEPARTMENT VALUES ('Finance');

INSERT INTO GRADELEVEL VALUES (30);
INSERT INTO GRADELEVEL VALUES (32);
INSERT INTO GRADELEVEL VALUES (35);
INSERT INTO GRADELEVEL VALUES (40);
INSERT INTO GRADELEVEL VALUES (45);
INSERT INTO GRADELEVEL VALUES (50);

INSERT INTO JOB VALUES ('Software Engineer I', 1);
INSERT INTO JOB VALUES ('Accountant', 1);
INSERT INTO JOB VALUES ('Software Engineer II', 2);
INSERT INTO JOB VALUES ('Software Engineer Senior', 3);
INSERT INTO JOB VALUES ('Project Manager', 4);
INSERT INTO JOB VALUES ('Project Manager Senior', 5);
INSERT INTO JOB VALUES ('Director', 6);

INSERT INTO SKILL VALUES ('HR');
INSERT INTO SKILL VALUES ('Accounting');
INSERT INTO SKILL VALUES ('Sales');
INSERT INTO SKILL VALUES ('PHP');
INSERT INTO SKILL VALUES ('C#');
INSERT INTO SKILL VALUES ('Java');
INSERT INTO SKILL VALUES ('JavaScript');
INSERT INTO SKILL VALUES ('Python');
INSERT INTO SKILL VALUES ('SQL Server Management Studio');
INSERT INTO SKILL VALUES ('Oracle');
INSERT INTO SKILL VALUES ('HTML');
INSERT INTO SKILL VALUES ('CSS');

INSERT INTO WORKINGSTATUS VALUES ('Working');
INSERT INTO WORKINGSTATUS VALUES ('Temporary Off');
INSERT INTO WORKINGSTATUS VALUES ('Resigned');

INSERT INTO EMPLOYEE VALUES ('009999331', 'Ken Le', '1993-11-09', 'Male', 'Single', null, '4704616870', '5927 Lanier Blvd, Norcross, GA 30071', 'kle22@student.gsu.edu', '2015-01-01', 'Thosini Mudiyanselage', 128, 3, 1, 2, 1);
INSERT INTO EMPLOYEE VALUES ('221199332', 'Justin Nguyen', '1993-08-21', 'Male', 'Married', null, '7707690999', '1283 Morrow Rd, Morrow, GA 30260', 'tnguyen417@student.gsu.edu', '2017-06-01', 'Binh Le', 128, 1, 2, 3, 1);
INSERT INTO EMPLOYEE VALUES ('228899113', 'Binh Le', '1991-11-28', 'Male', 'Married', null, '6784356280', '577 Watson Ferry Dr, Forest Park, GA 30297', 'ble8@student.gsu.edu', '2013-01-01', 'Thosini Mudiyanselage', 128, 2, 4, 5, 1);
INSERT INTO EMPLOYEE VALUES ('116699444', 'Jacob Nguyen', '1994-01-16', 'Male', 'Single', null, '7709057891', '1081 Rockbridge Rd, Norcross, GA 30093', 'dnguyen121@student.gsu.edu', '2015-09-01', 'Binh Le', 128, 1, 3, 4, 1);
INSERT INTO EMPLOYEE VALUES ('447711005', 'Thosini Mudiyanselage', '1991-01-05', 'Female', 'Single', null, '4040102020', '33 Gilmer St SE, Atlanta, GA 30303', 'tbamunumudiyanselag1@gsu.edu', '2010-01-01', null, 128, 2, 6, 7, 1);

INSERT INTO EMP_HAS_SKILL VALUES (1, 1);
INSERT INTO EMP_HAS_SKILL VALUES (1, 2);
INSERT INTO EMP_HAS_SKILL VALUES (1, 3);
INSERT INTO EMP_HAS_SKILL VALUES (2, 4);
INSERT INTO EMP_HAS_SKILL VALUES (2, 5);
INSERT INTO EMP_HAS_SKILL VALUES (2, 6);
INSERT INTO EMP_HAS_SKILL VALUES (3, 5);
INSERT INTO EMP_HAS_SKILL VALUES (3, 6);
INSERT INTO EMP_HAS_SKILL VALUES (3, 7);
INSERT INTO EMP_HAS_SKILL VALUES (3, 9);
INSERT INTO EMP_HAS_SKILL VALUES (3, 10);
INSERT INTO EMP_HAS_SKILL VALUES (3, 11);
INSERT INTO EMP_HAS_SKILL VALUES (3, 12);
INSERT INTO EMP_HAS_SKILL VALUES (4, 4);
INSERT INTO EMP_HAS_SKILL VALUES (4, 6);
INSERT INTO EMP_HAS_SKILL VALUES (4, 8);
INSERT INTO EMP_HAS_SKILL VALUES (4, 11);
INSERT INTO EMP_HAS_SKILL VALUES (4, 12);
INSERT INTO EMP_HAS_SKILL VALUES (5, 1);
INSERT INTO EMP_HAS_SKILL VALUES (5, 2);
INSERT INTO EMP_HAS_SKILL VALUES (5, 3);
INSERT INTO EMP_HAS_SKILL VALUES (5, 4);
INSERT INTO EMP_HAS_SKILL VALUES (5, 5);
INSERT INTO EMP_HAS_SKILL VALUES (5, 6);
INSERT INTO EMP_HAS_SKILL VALUES (5, 7);
INSERT INTO EMP_HAS_SKILL VALUES (5, 8);
INSERT INTO EMP_HAS_SKILL VALUES (5, 9);
INSERT INTO EMP_HAS_SKILL VALUES (5, 10);
INSERT INTO EMP_HAS_SKILL VALUES (5, 11);
INSERT INTO EMP_HAS_SKILL VALUES (5, 12);

INSERT INTO REASON_TIMEOFF VALUES ('Emergency');
INSERT INTO REASON_TIMEOFF VALUES ('Personal');
INSERT INTO REASON_TIMEOFF VALUES ('Sick');
INSERT INTO REASON_TIMEOFF VALUES ('Vacation');

INSERT INTO TYPE_TIMEOFF VALUES ('Paid');
INSERT INTO TYPE_TIMEOFF VALUES ('Unpaid');

INSERT INTO TIMEOFF VALUES ('2020-03-02', 8, 3, 1, 'flu', 3);
INSERT INTO TIMEOFF VALUES ('2020-03-03', -2, 1, 2, 'family situation', 2);
INSERT INTO TIMEOFF VALUES ('2020-03-04', -4, 2, 2, 'unexpected work', 4);
INSERT INTO TIMEOFF VALUES ('2020-03-05', 8, 4, 1, 'vacation', 1);
INSERT INTO TIMEOFF VALUES ('2020-03-06', 8, 4, 1, 'vacation', 1);

INSERT INTO HOLIDAYSCHEDULE VALUES ('2020-07-04', 'Independence Day', 12);
INSERT INTO HOLIDAYSCHEDULE VALUES ('2020-11-26', 'Thanksgiving Day', 16);
INSERT INTO HOLIDAYSCHEDULE VALUES ('2020-12-25', 'Christmas', 8);

INSERT INTO EMP_WORK_ON_HOLIDAY VALUES (1, 1);
INSERT INTO EMP_WORK_ON_HOLIDAY VALUES (1, 2);
INSERT INTO EMP_WORK_ON_HOLIDAY VALUES (2, 1);
INSERT INTO EMP_WORK_ON_HOLIDAY VALUES (2, 2);
INSERT INTO EMP_WORK_ON_HOLIDAY VALUES (3, 1);
INSERT INTO EMP_WORK_ON_HOLIDAY VALUES (3, 3);
INSERT INTO EMP_WORK_ON_HOLIDAY VALUES (4, 1);
INSERT INTO EMP_WORK_ON_HOLIDAY VALUES (4, 3);

INSERT INTO WEEKLYSALARY VALUES ('2020-03-01', '2020-03-07', 40, 1);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-01', '2020-03-07', 40, 2);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-01', '2020-03-07', 40, 3);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-01', '2020-03-07', 40, 4);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-01', '2020-03-07', 40, 5);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-08', '2020-03-14', 40, 1);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-08', '2020-03-14', 40, 2);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-08', '2020-03-14', 40, 3);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-08', '2020-03-14', 40, 4);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-08', '2020-03-14', 40, 5);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-15', '2020-03-21', 40, 1);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-15', '2020-03-21', 40, 2);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-15', '2020-03-21', 40, 3);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-15', '2020-03-21', 40, 4);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-15', '2020-03-21', 40, 5);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-22', '2020-03-28', 40, 1);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-22', '2020-03-28', 40, 2);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-22', '2020-03-28', 40, 3);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-22', '2020-03-28', 40, 4);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-22', '2020-03-28', 40, 5);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-29', '2020-04-04', 40, 1);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-29', '2020-04-04', 40, 2);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-29', '2020-04-04', 40, 3);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-29', '2020-04-04', 40, 4);
INSERT INTO WEEKLYSALARY VALUES ('2020-03-29', '2020-04-04', 40, 5);
INSERT INTO WEEKLYSALARY VALUES ('2020-04-05', '2020-04-11', 40, 1);
INSERT INTO WEEKLYSALARY VALUES ('2020-04-05', '2020-04-11', 40, 2);
INSERT INTO WEEKLYSALARY VALUES ('2020-04-05', '2020-04-11', 40, 3);
INSERT INTO WEEKLYSALARY VALUES ('2020-04-05', '2020-04-11', 40, 4);
INSERT INTO WEEKLYSALARY VALUES ('2020-04-05', '2020-04-11', 40, 5);
INSERT INTO WEEKLYSALARY VALUES ('2020-04-12', '2020-04-18', 40, 1);
INSERT INTO WEEKLYSALARY VALUES ('2020-04-12', '2020-04-18', 40, 2);
INSERT INTO WEEKLYSALARY VALUES ('2020-04-12', '2020-04-18', 40, 3);
INSERT INTO WEEKLYSALARY VALUES ('2020-04-12', '2020-04-18', 40, 4);
INSERT INTO WEEKLYSALARY VALUES ('2020-04-12', '2020-04-18', 40, 5);

INSERT INTO BONUS VALUES (1, 3);
INSERT INTO BONUS VALUES (2, 5);
INSERT INTO BONUS VALUES (3, 7);
INSERT INTO BONUS VALUES (4, 10);
INSERT INTO BONUS VALUES (5, 12);
INSERT INTO BONUS VALUES (6, 13);

INSERT INTO OVERTIME VALUES ('2020-03-11', 2, 'customer support', 3);
INSERT INTO OVERTIME VALUES ('2020-03-15', 1.5, 'go out with customer', 1);
INSERT INTO OVERTIME VALUES ('2020-03-18', 3, 'urgent project', 2);
INSERT INTO OVERTIME VALUES ('2020-03-18', 3, 'urgent project', 3);
INSERT INTO OVERTIME VALUES ('2020-03-18', 3, 'urgent project', 4);

INSERT INTO STATES VALUES ('AL', 'Alabama');
INSERT INTO STATES VALUES ('AK', 'Alaska');
INSERT INTO STATES VALUES ('AZ', 'Arizona');
INSERT INTO STATES VALUES ('AR', 'Arkansas');
INSERT INTO STATES VALUES ('CA', 'California');
INSERT INTO STATES VALUES ('CO', 'Colorado');
INSERT INTO STATES VALUES ('CT', 'Connecticut');
INSERT INTO STATES VALUES ('DE', 'Delaware');
INSERT INTO STATES VALUES ('FL', 'Florida');
INSERT INTO STATES VALUES ('GA', 'Georgia');
INSERT INTO STATES VALUES ('HI', 'Hawaii');
INSERT INTO STATES VALUES ('ID', 'Idaho');
INSERT INTO STATES VALUES ('IL', 'Illinois');
INSERT INTO STATES VALUES ('IN', 'Indiana');
INSERT INTO STATES VALUES ('IA', 'Iowa');
INSERT INTO STATES VALUES ('KS', 'Kansas');
INSERT INTO STATES VALUES ('KY', 'Kentucky');
INSERT INTO STATES VALUES ('LA', 'Louisiana');
INSERT INTO STATES VALUES ('ME', 'Maine');
INSERT INTO STATES VALUES ('MD', 'Maryland');
INSERT INTO STATES VALUES ('MA', 'Massachusetts');
INSERT INTO STATES VALUES ('MI', 'Michigan');
INSERT INTO STATES VALUES ('MN', 'Minnesota');
INSERT INTO STATES VALUES ('MS', 'Mississippi');
INSERT INTO STATES VALUES ('MO', 'Missouri');
INSERT INTO STATES VALUES ('MT', 'Montana');
INSERT INTO STATES VALUES ('NE', 'Nebraska');
INSERT INTO STATES VALUES ('NV', 'Nevada');
INSERT INTO STATES VALUES ('NH', 'New Hampshire');
INSERT INTO STATES VALUES ('NJ', 'New Jersey');
INSERT INTO STATES VALUES ('NM', 'New Mexico');
INSERT INTO STATES VALUES ('NY', 'New York');
INSERT INTO STATES VALUES ('NC', 'North Carolina');
INSERT INTO STATES VALUES ('ND', 'North Dakota');
INSERT INTO STATES VALUES ('OH', 'Ohio');
INSERT INTO STATES VALUES ('OK', 'Oklahoma');
INSERT INTO STATES VALUES ('OR', 'Oregon');
INSERT INTO STATES VALUES ('PA', 'Pennsylvania');
INSERT INTO STATES VALUES ('RI', 'Rhode Island');
INSERT INTO STATES VALUES ('SC', 'South Carolina');
INSERT INTO STATES VALUES ('SD', 'South Dakota');
INSERT INTO STATES VALUES ('TN', 'Tennessee');
INSERT INTO STATES VALUES ('TX', 'Texas');
INSERT INTO STATES VALUES ('UT', 'Utah');
INSERT INTO STATES VALUES ('VT', 'Vermont');
INSERT INTO STATES VALUES ('VA', 'Virginia');
INSERT INTO STATES VALUES ('WA', 'Washington');
INSERT INTO STATES VALUES ('WV', 'West Virginia');
INSERT INTO STATES VALUES ('WI', 'Wisconsin');
INSERT INTO STATES VALUES ('WY', 'Wyoming');

INSERT INTO RELATION VALUES ('Brother');
INSERT INTO RELATION VALUES ('Child');
INSERT INTO RELATION VALUES ('Father');
INSERT INTO RELATION VALUES ('Friend');
INSERT INTO RELATION VALUES ('Grandparent');
INSERT INTO RELATION VALUES ('Guardian');
INSERT INTO RELATION VALUES ('Mother');
INSERT INTO RELATION VALUES ('Sister');
INSERT INTO RELATION VALUES ('Sponsor');
INSERT INTO RELATION VALUES ('Spouse');
INSERT INTO RELATION VALUES ('Other');

INSERT INTO BENEFICIARY VALUES ('Andy', 'Le', 1, '4041000073', '5927 Lanier Blvd', 'Norcross', 'GA', '30071', 'United States of America', null, 1);
INSERT INTO BENEFICIARY VALUES ('Ellie', 'Nguyen', 10, '4042001826', '1283 Morrow Rd', 'Morrow', 'GA', '30260', 'United States of America', 'pjg_3pie_luv_honey@yahoo.com', 2);
INSERT INTO BENEFICIARY VALUES ('Alice', 'Nguyen', 2, null, '1283 Morrow Rd', 'Morrow', 'GA', '30260', 'United States of America', null, 2);
INSERT INTO BENEFICIARY VALUES ('Phuong', 'Huynh', 10, '6783000319', '577 Watson Ferry Dr', 'Forest Park', 'GA', '30297', 'United States of America', 'phuonghtm190314@gmail.com', 3);
INSERT INTO BENEFICIARY VALUES ('John', 'Lai', 1, '4043000118', '392 Springside Dr', 'Forest Park', 'GA', '30297', 'United States of America', 'trunganh39@gmail.com', 3);
INSERT INTO BENEFICIARY VALUES ('Emily', 'Nguyen', 8, '6784004479', '1081 Rockbridge Rd', 'Norcross', 'GA', '30093', 'United States of America', 'nhu8285@yahoo.com', 4);
INSERT INTO BENEFICIARY VALUES ('Rathnayaka', 'Mudiyanselage', 7, '4040104710', '33 Gilmer St SE', 'Atlanta', 'GA', '30303', 'United States of America', null, 5);