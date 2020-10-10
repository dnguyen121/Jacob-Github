/*
	Project: Gauss Elimination & Back Substitution
	Full name: Jacob Nguyen
	Class: Math 3030
	Professor: Dr. Kelvin Rozier
*/
import java.util.*;
import java.io.*;
public class Test {
	public static void main(String[] args) {
	
		double[][] matrixA = {{2,-2,4,0,0},
							  {-3,3,-6,5,15},
							  {1,-1,2,0,0}};
		
		double[][] matrixB = {{2,3,1,-11,1},
				 			  {5,-2,5,-4,5},
				 			  {1,-1,3,-3,3},
				 			  {3,4,-7,2,-7}};
		
		GaussElimination gauss = new GaussElimination();
		gauss.solution(matrixA);
		printLine();
		gauss.solution(matrixB);

	}
	
	public static void printLine() {
		System.out.println();
		System.out.println("----------------------------------------------------");
		System.out.println();
	}

}
