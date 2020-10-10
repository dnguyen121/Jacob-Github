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