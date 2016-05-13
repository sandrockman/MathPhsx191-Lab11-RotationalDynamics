using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/**
 * @author Victor Haskins
 * class Program used to facilitate Lab 11 instructions
 */
class Program
{
    static void Main(string[] args)
    {

        StreamWriter writer1 = null;
        string file = "oneForTheBooks.csv";
        try
        {
            writer1 = new StreamWriter(File.Open(file, FileMode.OpenOrCreate));
        }
        catch(Exception ex)
        {
            Console.WriteLine("oops: "+ ex);
        }
        //details for ball one
        double mass1 = 1.2; //in kg
        double mass2 = 4.9; //in kg
        double r = 0.35; //in meters
        double angle = 30; //in degrees
        double angRad = angle * Math.PI / 180; //in radians

        double timeStep = 0.1; //in seconds
        double time = 0;

        Vector3D centerOfMass = new Vector3D(); // starts at zero vector
        Vector3D linearVelocity = new Vector3D();
        double angularDisplacement = 0;
        double angularVelocity = 0;
        double angularAcceleration = 0;

        //find ball one distance from center of mass.
        //assuming first that ball 1 will be the center, find COM.
        double ball1 = (mass2 * r / (mass1 + mass2)); //DistFromCOM in neg x
        //find ball two distance from center of mass. 
        double ball2 = r - ball1; //DistFromCOM in pos x

        double inertiaMoment = (mass1 * ball1 * ball1) + (mass2 * ball2 * ball2);


        //user supplied variables
        double inputTime = 0;
        Console.Write("Please enter magnitude of the Force applied: ");
        double systemForce = Convert.ToDouble(Console.ReadLine());

        Vector3D forceVector = new Vector3D((systemForce * Math.Cos(angRad)),
                                           (systemForce * Math.Sin(angRad)),
                                            0, 1);

        Vector3D accelVector = (1 / (mass1 + mass2)) & forceVector;

        Console.Write("Please enter time of system in seconds: ");
        inputTime = Convert.ToDouble(Console.ReadLine());

        while(time < inputTime)
        {
            
            Console.Write("Time: " + time + ", pos: (" +
                centerOfMass.GetX() + ", " + centerOfMass.GetY() + ")");
            Console.WriteLine(", velocity: (" +
                linearVelocity.GetX() + ", " + linearVelocity.GetY() + ")");
            Console.Write("and AngD: "+angularDisplacement);
            Console.Write(", AngV: " + angularVelocity);
            Console.WriteLine(", AngA: " + angularAcceleration);
            
            writer1.WriteLine(time + ",Pos," + centerOfMass.GetX() + "," + centerOfMass.GetY() + ",Vel," + 
                linearVelocity.GetX() + "," + linearVelocity.GetY() + ",theta," + angularDisplacement +
                ",omega," + angularVelocity + ",alpha," + angularAcceleration);
            //Linear
            centerOfMass += linearVelocity & timeStep;
            linearVelocity += accelVector & timeStep;
            //no change to linear velocity or linear acceleration.
            //Angular
            angularDisplacement += (angularVelocity * timeStep);
            angularVelocity += (angularAcceleration * timeStep);
            //alpha = torque / moment of inertia ; torque = radius * Force * sin(theta) ; moment of inertia = (m1*r1^2 + m2*r2^2)
            angularAcceleration = (ball1 * systemForce * Math.Sin(angRad - angularDisplacement))/inertiaMoment;
            time += timeStep;
        }
        writer1.Close();
    }

    static double D2R(double degrees)
    {
        return degrees * Math.PI / (double)180;
    }

    static double R2D(double radians)
    {
        return radians * (double)180 / Math.PI;
    }
}
