/* Personal Information - get information of the employee when login - show personal information */
SELECT e.employee_id, e.employee_name, e.employee_dob, e.employee_sex, e.employee_maritalStatus,e.employee_image, 
       e.employee_phone, e.employee_address, e.employee_mail, e.employee_hireDate, e.employee_superiorName, e.employee_balances, 
	   d.department_name, g.gradelevel_payRate, j.job_name, w.workingstatus_name
FROM EMPLOYEE e, DEPARTMENT d, JOB j, GRADELEVEL g, WORKINGSTATUS w
WHERE e.employee_dept_id = d.department_id AND e.employee_jb_id = j.job_id AND e.employee_gralv_id = g.gradelevel_id AND 
      e.employee_workstt_id = w.workingstatus_id AND e.employee_mail = 'ble8@student.gsu.edu' /* @employee_mail = 'ble8@student.gsu.edu' */

/* Personal Information - update image */
UPDATE EMPLOYEE 
SET employee_image = '/Content/images/image04192020042937719.jpg' /* @employee_image = '/Content/images/image04192020042937719.jpg' */
WHERE employee_id = 3

/* Career - get skills of the employee - show skills */
SELECT s.skill_id, s.skill_name 
FROM EMPLOYEE e, EMP_HAS_SKILL es, SKILL s
WHERE e.employee_id = es.emp_id AND s.skill_id = es.skl_id AND es.emp_id = 3 /* @id = 3 */

/* Career - get skills that employees do not have - options when employees add new skill */ 
SELECT s1.skill_id, s1.skill_name 
FROM SKILL s1 
WHERE s1.skill_id IN (SELECT s2.skill_id 
					  FROM SKILL s2 
					  EXCEPT 
					  SELECT skl_id 
					  FROM EMP_HAS_SKILL 
					  WHERE emp_id = 3) /* @id = 3 */

/* Career - add new skill */
INSERT INTO EMP_HAS_SKILL(emp_id, skl_id) VALUES (3, 8) /* @id = 3, @sklId = 8 */

/* Career - delete skills */ 
DELETE FROM EMP_HAS_SKILL 
WHERE emp_id = 3 AND skl_id = 8 /* @id = 3, @sklId = 8 */

/* Weekly Salary - get pay rate of the employee - calculate salary */
SELECT gl.gradelevel_id, gl.gradelevel_payRate 
FROM EMPLOYEE e, GRADELEVEL gl 
WHERE e.employee_gralv_id = gl.gradelevel_id AND e.employee_id = 3 /* @id = 3 */

/* Time Off - summit time off from the employee */
	/* if timeoff_tpe_id = 1 (paid) */
	INSERT INTO TIMEOFF VALUES ('2020-04-21', 8, 3, 1, 'COVID-19', 3);
	
	UPDATE EMPLOYEE 
	SET employee_balances = employee_balances - 8 /* @timeoff_numberOfHours = 8 */
	WHERE employee_id = 3 /* @id = 3 */

	/* if timeoff_tpe_id = 2 (unpaid) */
	INSERT INTO TIMEOFF VALUES ('2020-04-20', -2, 1, 2, 'heavy traffic', 3);

/* Organization - get subordinate of the employee */
SELECT e1.employee_name, e1.employee_phone, j.job_name
FROM EMPLOYEE e1, JOB j
WHERE e1.employee_jb_id = j.job_id AND e1.employee_superiorName IN (SELECT e2.employee_name
																    FROM EMPLOYEE e2
																    WHERE e2.employee_id = 3) /* @id = 3 */
/* Organization - get superior of the employee */
SELECT e1.employee_name, e1.employee_phone, j.job_name
FROM EMPLOYEE e1, JOB j
WHERE e1.employee_jb_id = j.job_id AND e1.employee_name IN (SELECT e2.employee_superiorName
														    FROM EMPLOYEE e2
														    WHERE employee_id = 3) /* @id = 3 */
/* Organization - get same level of the employee */
SELECT e1.employee_name, e1.employee_phone, j.job_name
FROM EMPLOYEE e1, JOB j
WHERE e1.employee_jb_id = j.job_id AND e1.employee_superiorName IN (SELECT e2.employee_superiorName
																	FROM EMPLOYEE e2
																	WHERE employee_id = 3) /* @id = 3 */
EXCEPT
SELECT e.employee_name, e.employee_phone, j.job_name
FROM EMPLOYEE e, JOB j
WHERE e.employee_jb_id = j.job_id AND e.employee_id = 3 /* @id = 3 */

/* Relationship - retrieve relative information of the employee - show relationship */
SELECT *
FROM BENEFICIARY
WHERE beneficiary_emp_id = 3 /* @id = 3 */

/* Relationship - update relationship */
UPDATE BENEFICIARY 
SET beneficiary_fName = 'Trung' /* @beneficiary_fName = 'Trung' */
WHERE beneficiary_id = 5 /* @beneficiary_id = 5 */

/* Relationship - delete relationship */
DELETE FROM BENEFICIARY 
WHERE beneficiary_id = 5 /* @beneficiary_id = 5 */

/* Relationship - add new relationship */
INSERT INTO BENEFICIARY VALUES ('John', 'Lai', 1, '4043000118', '392 Springside Dr', 'Forest Park', 'GA', '30297', 'United States of America', 'trunganh39@gmail.com', 3);

/* HR Management - add new employee */
INSERT INTO EMPLOYEE VALUES ('123456789', 'Chunyan Ji', '1992-12-12', 'Female', 'Single', null, '4046781234', '33 Gilmer St SE, Atlanta, GA 30303', 'cji2@student.gsu.edu', '2010-04-20', 'Thosini Mudiyanselage', 128, 2, 4, 5, 1);

/* Overtime - add new overtime for the employee */
INSERT INTO OVERTIME VALUES ('2020-04-20', 8, 'support project', 3);