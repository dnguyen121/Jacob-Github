/*
	Project: Gauss Elimination & Back Substitution
	Full name: Jacob Nguyen
	Class: Math 3030
	Professor: Dr. Kelvin Rozier
*/
import java.util.*;
public class GaussElimination {
	public static void solution(double[][] matrix) {
		printPreMatrix(matrix);
		elimination(matrix);
		solveMatrix(matrix);
	}
	
	public static void elimination(double[][] matrix) {
		for(int i = 0; i < matrix.length-1;i++) {
			for(int k = i+1; k < matrix.length;k++) {
				double a = matrix[i][i];
				double b = matrix[k][i];
				if(a == 0 || b == 0) {
					break;
				}
				else {
					for(int j = i; j < matrix[i].length;j++) {
						matrix[i][j] = matrix[i][j]*b;
						matrix[k][j] = matrix[k][j]*a;
						matrix[k][j] = matrix[i][j]-matrix[k][j];
						matrix[i][j] = matrix[i][j]/b;

					}
				}
			}
		}
	}
	
	public static void solveMatrix(double[][] matrix) {
		boolean noSol = false;
		boolean infinite = false;
		for(int i = 0; i <= matrix.length-1; i++) {
			double sum = 0.0;
			for(int j = 0; j <= matrix[i].length-2; j++) {
				sum += matrix[i][j];
				if(sum != 0) {
					break;
				}
			}
			if(sum == 0 && matrix[i][matrix[i].length-1] !=0) {
				noSol = true;
			}
			else if ( sum == 0 && matrix[i][matrix[i].length-1] == 0) {
				infinite = true;
			}
		}
		if(noSol) {
			printPostMatrix(matrix);
			System.out.println();
			System.out.println("The system is inconsistent.");
		}
		else if (infinite || (matrix[0].length > matrix.length+1)) {
			printPostMatrix(matrix);
			System.out.println();
			System.out.println("The system is consistent and have infinite solution.");
			
		}
		else {
			printPostMatrix(matrix);
			System.out.println();
			System.out.println("The system is consistent and have one solution.");
			backSub(matrix);
		}
	}
	
	public static void backSub(double[][] matrix) {
		double[] sol = new double[matrix.length];
		for(int i = matrix.length-1; i >=0; i--) {
			double sum = 0.0;
			for(int j = i+1; j < matrix.length; j++) {
				sum += matrix[i][j] * sol[j];
			}
			sol[i] = (matrix[i][matrix.length]-sum)/matrix[i][i];
		}
		System.out.println("The solution is");
		for(int i = 0; i < sol.length; i++) {
			System.out.println("x"+(i+1)+" = "+sol[i]);
		}
	}
	
	public static void printPostMatrix(double[][] matrix) {
		System.out.println();
		for(int i = 0; i < matrix.length; i++) {
			boolean multiple = true;
			double min = Math.abs(matrix[i][0]);
			for(int j = 0; j < matrix[i].length; j++) {
				if(matrix[i][j] != 0) {
					min = matrix[i][j];
					break;
				}
			}
			for(int j = 0; j < matrix[i].length; j++) {
				if(matrix[i][j] != 0) {
					min = Math.min(Math.abs(matrix[i][j]),min);
				}
			}
			if(min == 0) {
				continue;
			}
			else {
				for(int j = 0; j < matrix[i].length; j++) {
					double a = matrix[i][j]%min;
					if(a != 0) {
						multiple = false;
						break;
					}
				}
			}
			if(multiple) {
				for(int j = 0; j < matrix[i].length; j++) {
					matrix[i][j] = matrix[i][j]/Math.abs(min);
				}
			}
		}
		System.out.println("The resultant maxtrix after Gauss elimination is: ");
		for(double[] a : matrix) {
			System.out.println(Arrays.toString(a));
		}
	}
	
	public static void printPreMatrix(double[][] matrix) {
		System.out.println("Matrix: ");
		for(double[] a : matrix) {
			System.out.println(Arrays.toString(a));
		}
	}
}
