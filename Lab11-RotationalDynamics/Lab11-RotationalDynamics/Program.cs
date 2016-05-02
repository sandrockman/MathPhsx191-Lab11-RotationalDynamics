using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * @author Victor Haskins
 * class Program used to facilitate Lab 11 instructions
 */
class Program
{
    static void Main(string[] args)
    {
        //details for ball one
        double mass1 = 1.2; //in kg
        double mass2 = 4.9; //in kg
        double r = 0.35; //in meters
        double angle = 30; //in degrees
        double angRad = angle * Math.PI / 180; //in radians

        double timeStep = 0.1; //in seconds

        Vector3D centerOfMass = new Vector3D(); // starts at zero vector
        Vector3D rotationalCenter = new Vector3D();

        //find ball one distance from center of mass.
        //assuming first that ball 1 will be the center, find COM.
        double ball1 = (mass2 * r / (mass1 + mass2)); //DistFromCOM in neg x
        //find ball two distance from center of mass. 
        double ball2 = r - ball1; //DistFromCOM in pos x

        double inertiaMoment = (mass1 * ball1 * ball1) + (mass2 * ball2 * ball2);


        //user supplied variables
        Vector3D force = new Vector3D();
        double time = 0;
        double inputTime = 0;
        Console.Write("Please enter x of Force: ");
        double tempX = Convert.ToDouble(Console.ReadLine());
        Console.Write("Please enter y of Force: ");
        double tempY = Convert.ToDouble(Console.ReadLine());

        Console.Write("Please enter time of system in seconds: ");
        inputTime = Convert.ToDouble(Console.ReadLine());

        while(time < inputTime)
        {

        }
    }
}
