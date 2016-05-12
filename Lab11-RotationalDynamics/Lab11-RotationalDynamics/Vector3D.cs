using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * @author Victor Haskins
 * class Vector3D stores Vector information that can be retrieved and printed
 * in rectangular, angular, and Euler methods
 */
public class Vector3D
{
    public double XValue { get; private set; }
    public double YValue { get; private set; }
    public double ZValue { get; private set; }
    public double WValue { get; private set; }

    /// <summary>
    /// Default constructor for the Vector3D class to be modified later
    /// </summary>
    public Vector3D()
    {
        XValue = 0;
        YValue = 0;
        ZValue = 0;
        WValue = 1;
    }

    /// <summary>
    /// takes 2D rectangular coordinates for saving into Vector3D
    /// </summary>
    /// <param name="newX">x value to be saved</param>
    /// <param name="newY">y value to be saved</param>
    public void SetRectGivenRect(double newX, double newY)
    {
        XValue = newX;
        YValue = newY;
    }

    /// <summary>
    /// takes rectangular coordinates for saving into Vector3D
    /// </summary>
    /// <param name="newX"></param>
    /// <param name="newY"></param>
    /// <param name="newZ"></param>
    public void SetRectGivenRect(double newX, double newY, double newZ)
    {
        XValue = newX;
        YValue = newY;
        ZValue = newZ;
    }

    //*********************NEW FOR SCALING/TRANSFORMATIONS*********************
    /// <summary>
    /// takes rectangular coordinates for saving into Vector3D with new param W
    /// for translation and scaling work
    /// </summary>
    /// <param name="newX"></param>
    /// <param name="newY"></param>
    /// <param name="newZ"></param>
    /// <param name="newW"></param>
    public Vector3D(double newX, double newY, double newZ, double newW)
    {
        XValue = newX;
        YValue = newY;
        ZValue = newZ;
        WValue = newW;
    }

    /// <summary>
    /// Takes the magnitude and heading, converts it into
    /// rectangular coordinates for saving.
    /// </summary>
    /// <param name="magnitude"></param>
    /// <param name="heading"></param>
    public void SetRectGivenPolar(double magnitude, double heading)
    {
        XValue = magnitude * Math.Cos(heading);
        YValue = magnitude * Math.Sin(heading);
    }

    /// <summary>
    /// Takes the magnitude, heading and pitch, converts it into
    /// rectangular coordinates for saving.
    /// </summary>
    /// <param name="magnitude"></param>
    /// <param name="heading"></param>
    /// <param name="pitch"></param>
    public void SetRectGivenMagHeadPitch(double magnitude,
        double heading, double pitch)
    {
        XValue = magnitude * Math.Cos(pitch) * Math.Cos(heading);
        YValue = magnitude * Math.Cos(pitch) * Math.Sin(heading);
        ZValue = magnitude * Math.Sin(pitch);
    }

    /// <summary>
    /// Prints the rectangular coordinates
    /// </summary>
    public void PrintRect()
    {
        Console.WriteLine("Rectangular Vector Coordinates: " +
            "< {0:F2} , {1:F2} , {2:F2} >", XValue, YValue, ZValue);
    }

    /// <summary>
    /// Prints the Magnitude, Heading and Pitch to the screen
    /// </summary>
    public void PrintMagHeadPitch()
    {
        Console.WriteLine("Magnitude: {0:F5}", GetMagnitude());
        if (GetMagnitude() == 0)
        {
            Console.WriteLine("Because magnitude is zero, " +
                "produced zero vector.");
        }
        else
        {
            if (GetMagnitude("twoD") == 0)
            {
                Console.WriteLine("Because 2D, xy magnitude is zero, " +
                "would produce error.");
            }
            else
            {
                Console.WriteLine("Heading: {0:F5} degrees", GetHeading());
            }
            Console.WriteLine("Pitch: {0:F5} degrees", GetPitch());
        }
    }

    /// <summary>
    /// prints the Euler angles
    /// </summary>
    public void PrintAngles()
    {
        Console.WriteLine("Euler Angles");
        if (GetMagnitude() != 0)
        {
            Console.WriteLine("Alpha: {0:F5}", GetAlpha());
            Console.WriteLine("Beta: {0:F5}", GetBeta());
            Console.WriteLine("Gamma: {0:F5}", GetGamma());
        }
        else
        {
            Console.WriteLine("Magnitude is zero, so answers are invalid.");
            Console.WriteLine("Should return:\nAlpha: 0");
            Console.WriteLine("Beta: 0");
            Console.WriteLine("Gamma: 0");
        }
    }

    /// <summary>
    /// returns the Magnitude, essentially the distance of the line
    /// from the origin point to the adjusted endpoint, showing the vector
    /// </summary>
    /// <returns>magnitude of vector</returns>
    public double GetMagnitude()
    {
        return Math.Sqrt(Math.Pow(XValue, 2) +
                         Math.Pow(YValue, 2) +
                         Math.Pow(ZValue, 2));
    }

    /// <summary>
    /// special case code to overload GetMagnitude. This will automatically
    /// return only the magnitude on the XY plane. Error checking if
    /// someone tries to break it.
    /// </summary>
    /// <param name="twoD"></param>
    /// <returns>magnitude of XY plane of vector in most cases</returns>
    public double GetMagnitude(string twoD)
    {
        //in case of error, will redirect to full magnitude, but overloading
        //should render this moot.
        if (String.IsNullOrEmpty(twoD))
            return GetMagnitude();
        else
            return Math.Sqrt(Math.Pow(XValue, 2) +
                             Math.Pow(YValue, 2));
    }
    /// <summary>
    /// calculates and returns the Pitch, Z angle off of the XY plane
    /// </summary>
    /// <returns>Pitch</returns>
    public double GetPitch()
    {
        //returns zero if magnitude results in a zero vector
        if (GetMagnitude() == 0)
            return 0;
        else
            return Math.Asin(ZValue / GetMagnitude()) * 180 / Math.PI;
    }

    /// <summary>
    /// calculates and returns the Heading, the angle on an XY plane
    /// that is off of a designated x positive origin rotating towards a
    /// positive y origin and continues.
    /// </summary>
    /// <returns>Heading</returns>
    public double GetHeading()
    {
        //returns zero if 2D magnitude results in a zero vector
        if (GetMagnitude("twoD") == 0)
            return 0;
        else
        {
            if (YValue < 0)//angle should appear in quadrant 3 or 4 sub. angle from 360
                return 360 - Math.Abs(Math.Acos(XValue / GetMagnitude("twoD")) * 180 / Math.PI);
            else//quadrant 1 or two
                return Math.Acos(XValue / GetMagnitude("twoD")) * 180 / Math.PI;
        }
    }

    /// <summary>
    /// returns the alpha angle for a vector
    /// </summary>
    /// <returns>alpha angle</returns>
    public double GetAlpha()
    {
        //returns zero if magnitude results in a zero vector
        if (GetMagnitude() == 0)
            return 0;
        else
            return Math.Acos(XValue / GetMagnitude()) * 180 / Math.PI;
    }

    /// <summary>
    /// Returns the Beta angle for a vector
    /// </summary>
    /// <returns>Beta angle in degrees</returns>
    public double GetBeta()
    {
        //returns zero if magnitude results in a zero vector
        if (GetMagnitude() == 0)
            return 0;
        else
            return Math.Acos(YValue / GetMagnitude()) * 180 / Math.PI;
    }

    /// <summary>
    /// Returns the Gamma Value
    /// </summary>
    /// <returns>gamma angle in degrees</returns>
    public double GetGamma()
    {
        //returns zero if magnitude results in a zero vector
        if (GetMagnitude() == 0)
            return 0;
        else
            return Math.Acos(ZValue / GetMagnitude()) * 180 / Math.PI;
    }

    /// <summary>
    /// returns the XValue
    /// </summary>
    /// <returns>XValue</returns>
    public double GetX()
    {
        return XValue;
    }

    /// <summary>
    /// returns the YValue
    /// </summary>
    /// <returns>YValue</returns>
    public double GetY()
    {
        return YValue;
    }

    /// <summary>
    /// returns the ZValue
    /// </summary>
    /// <returns>ZValue</returns>
    public double GetZ()
    {
        return ZValue;
    }

    /// <summary>
    /// returns the WValue
    /// </summary>
    /// <returns>ZValue</returns>
    public double GetW()
    {
        return WValue;
    }

    /// <summary>
    /// Returns the sum of two vectors
    /// </summary>
    /// <param name="vector1">first vector</param>
    /// <param name="vector2">second vector</param>
    /// <returns>vector3D</returns>
    public static Vector3D operator +(Vector3D vector1, Vector3D vector2)
    {
        Vector3D newVector = new Vector3D();
        newVector.SetRectGivenRect(vector1.XValue + vector2.XValue,
                                   vector1.YValue + vector2.YValue,
                                   vector1.ZValue + vector2.ZValue);

        return newVector;
    }

    /// <summary>
    /// Returns the difference of two vectors. Order is important!
    /// </summary>
    /// <param name="vector1">vector to be subtracted from</param>
    /// <param name="vector2">vector to subtract</param>
    /// <returns>vector3D</returns>
    public static Vector3D operator -(Vector3D vector1, Vector3D vector2)
    {
        Vector3D newVector = new Vector3D();
        newVector.SetRectGivenRect(vector1.XValue - vector2.XValue,
                                   vector1.YValue - vector2.YValue,
                                   vector1.ZValue - vector2.ZValue);

        return newVector;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scalar">scalar variable</param>
    /// <param name="vector1">vector to be scaled</param>
    /// <returns>Vector3D</returns>
    public static Vector3D operator &(double scalar, Vector3D vector1)
    {
        Vector3D newVector = new Vector3D();
        newVector.SetRectGivenRect(vector1.XValue * scalar,
                                   vector1.YValue * scalar,
                                   vector1.ZValue * scalar);

        return newVector;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vector1"></param>
    /// <param name="scalar"></param>
    /// <returns></returns>
    public static Vector3D operator &(Vector3D vector1, double scalar)
    {
        Vector3D newVector = new Vector3D();
        newVector.SetRectGivenRect(vector1.XValue * scalar,
                                   vector1.YValue * scalar,
                                   vector1.ZValue * scalar);

        return newVector;
    }

    /// <summary>
    /// Returns the normalization of the vector
    /// </summary>
    /// <param name="vector1"></param>
    /// <returns></returns>
    public static Vector3D operator !(Vector3D vector1)
    {
        Vector3D newVector = new Vector3D();
        double magnitude = Math.Sqrt((vector1.XValue * vector1.XValue) +
                                     (vector1.YValue * vector1.YValue) +
                                     (vector1.ZValue * vector1.ZValue));
        if (magnitude == 0)
            return newVector;
        else
        {
            newVector.SetRectGivenRect(vector1.XValue / magnitude,
                                       vector1.YValue / magnitude,
                                       vector1.ZValue / magnitude);
            return newVector;
        }
    }

    /// <summary>
    /// dot product of two vectors
    /// </summary>
    /// <param name="vector1">first vector</param>
    /// <param name="vector2">second vector</param>
    /// <returns>double</returns>
    public static double operator *(Vector3D vector1, Vector3D vector2)
    {
        double dot = vector1.XValue * vector2.XValue +
                     vector1.YValue * vector2.YValue +
                     vector1.ZValue * vector2.ZValue;

        return dot;
    }

    /// <summary>
    /// Returns the angle between two vectors from u to v
    /// </summary>
    /// <param name="vector1">u vector</param>
    /// <param name="vector2">v vector</param>
    /// <returns>double</returns>
    public static double operator %(Vector3D vector1, Vector3D vector2)
    {
        double angle = 0;

        angle = Math.Acos(!vector1 * !vector2) * 180 / Math.PI;

        return angle;
    }

    /// <summary>
    /// perpendicular projection of vector 2 onto vector 1, proj v of u
    /// </summary>
    /// <param name="vector1">u vector</param>
    /// <param name="vector2">v vector</param>
    /// <returns>Vector3D</returns>
    public static Vector3D operator |(Vector3D vector1, Vector3D vector2)
    {
        Vector3D newVector = new Vector3D();
        newVector = vector2 - (vector1 ^ vector2);

        return newVector;
    }

    /// <summary>
    /// parallel projection of vector 2 onto vector 1, proj v of u
    /// </summary>
    /// <param name="vector1">u vector</param>
    /// <param name="vector2">v vector</param>
    /// <returns>Vector3D</returns>
    public static Vector3D operator ^(Vector3D vector1, Vector3D vector2)
    {
        Vector3D newVector = new Vector3D();

        newVector = (vector2 * !vector1) & !vector1;

        /*//alt. method of acquiring parallel projection.
        double vec1Mag = vector1.GetMagnitude();
        double vecAngle = vector2 % vector1;
        newVector = (vec1Mag * Math.Cos(vecAngle)) & !newVector;
        */
        return newVector;
    }

    //**********Important! Start of work for LAB07 - CLOSEST POINTS************

    /// <summary>
    /// using the class instance of Vector3D as the point that will be checked, find the 
    /// point on the Line that is closest to the checked point in space.
    /// </summary>
    /// <pre>class instance of Vector3D as the point that will be checked against the Line</pre>
    /// <param name="lineStart">point in space given as a vector.</param>
    /// <param name="lineDirection"></param>
    /// <returns>Vector3D point on line starting at lineStart in direction of lineDirection</returns>
    public Vector3D ClosestPointOnLine(Vector3D lineStart, Vector3D lineDirection)
    {
        //for ease of use with the math work from class,
        //Q = pointCheck or class instance given a new form here that we will exploit for return purposes.
        //P = lineStart entered as a Vector3D Class instance for easy manipulation
        //d or direction vector = line direction as a Vector3D.

        Vector3D pointCheck = new Vector3D();
        pointCheck.SetRectGivenRect(XValue, YValue, ZValue);

        //set vector PQ by subtracting P from Q or PQ = Q - P
        Vector3D PQ = pointCheck - lineStart;
        //(projection of PQ onto D)
        Vector3D projOfPQOnD = lineDirection ^ PQ;
        //P + (projection of PQ onto D)
        return lineStart + projOfPQOnD;
    }


    /// <summary>
    /// using the class instance of Vector3D as the point that will be checked, find the 
    /// point on the Line that is closest to the checked point in space.
    /// </summary>
    /// <pre>assuming the parameters entered form a plane and not just a line.</pre>
    /// <param name="point1">first point to find a plane</param>
    /// <param name="point2">second point to find a plane</param>
    /// <param name="point3">third point to find a plane</param>
    /// <returns>point on plane as Vector3D instance that is closest to point to check</returns>
    public Vector3D ClosestPointOnPlane(Vector3D point1, Vector3D point2, Vector3D point3)
    {
        //Represented as point Q in the math work, this is the point that is 
        //checked against the plane for the closest point.
        //P can be substituted by point1, but any of the points would have worked.
        Vector3D pointCheck = new Vector3D();
        pointCheck.SetRectGivenRect(XValue, YValue, ZValue);
        //first plane vector
        Vector3D point1to2 = point2 - point1;
        //second plane vector
        Vector3D point1to3 = point3 - point1;
        //plane normal vector
        Vector3D normalVector = point1to2 / point1to3;

        Vector3D PQ = pointCheck - point1;

        Vector3D projOfPQonNormal = normalVector ^ PQ;

        return pointCheck - projOfPQonNormal;
    }

    /// <summary>
    /// find the normal vector of two vectors given
    /// </summary>
    /// <param name="vector1">first vector</param>
    /// <param name="vector2">second vector</param>
    /// <returns>normal vector</returns>
    public static Vector3D operator /(Vector3D vector1, Vector3D vector2)
    {
        //V1 X V2
        //<x1, y1, z1> X <x2, y2, z2>
        //used to find the normal <i, j, k>
        //i = y1*z2 - z1*y2
        double i = (vector1.YValue * vector2.ZValue) - (vector1.ZValue * vector2.YValue);
        //j = -(x1z2 - z1x2) = -x1z2 + z1x2
        double j = (vector1.ZValue * vector2.XValue) - (vector1.XValue * vector2.ZValue);
        //k = x1y2 - y1x2
        double k = (vector1.XValue * vector2.YValue) - (vector1.YValue * vector2.XValue);

        Vector3D normal = new Vector3D();
        normal.SetRectGivenRect(i, j, k);

        return normal;
    }

    public static double dotProduct4D(Vector3D rowU, Vector3D colV)
    {
        return (rowU.XValue * colV.XValue) + (rowU.YValue * colV.YValue) +
            (rowU.ZValue * colV.ZValue) + (rowU.WValue * colV.WValue);
    }
    /// <summary>
    /// Translates the object List by 
    /// </summary>
    /// <param name="objList"></param>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static List<Vector3D> Translation(List<Vector3D> objList, Vector3D matrix)
    {
        //to be listed as columns
        List<Vector3D> translatedObj = new List<Vector3D>();

        //Listed as rows
        List<Vector3D> tMatrix = new List<Vector3D>();
        tMatrix.Add(new Vector3D(1, 0, 0, matrix.XValue));
        tMatrix.Add(new Vector3D(0, 1, 0, matrix.YValue));
        tMatrix.Add(new Vector3D(0, 0, 1, matrix.ZValue));
        tMatrix.Add(new Vector3D(0, 0, 0, 1));

        foreach (Vector3D column in objList)
        {
            Vector3D temp = new Vector3D(
                dotProduct4D(tMatrix[0], column),
                dotProduct4D(tMatrix[1], column),
                dotProduct4D(tMatrix[2], column),
                dotProduct4D(tMatrix[3], column));
            translatedObj.Add(temp);
        }

        return translatedObj;
    }

    public static List<Vector3D> RawScaling(List<Vector3D> objList, Vector3D matrix)
    {
        //to be listed as columns
        List<Vector3D> scaledObj = new List<Vector3D>();

        //Listed as rows
        List<Vector3D> sMatrix = new List<Vector3D>();
        sMatrix.Add(new Vector3D(matrix.XValue, 0, 0, 0));
        sMatrix.Add(new Vector3D(0, matrix.YValue, 0, 0));
        sMatrix.Add(new Vector3D(0, 0, matrix.ZValue, 0));
        sMatrix.Add(new Vector3D(0, 0, 0, 1));

        foreach (Vector3D column in objList)
        {
            Vector3D temp = new Vector3D(
                dotProduct4D(sMatrix[0], column),
                dotProduct4D(sMatrix[1], column),
                dotProduct4D(sMatrix[2], column),
                dotProduct4D(sMatrix[3], column));
            scaledObj.Add(temp);
        }

        return scaledObj;
    }

    public static List<Vector3D> CenterScaling(List<Vector3D> objList, Vector3D scaleMatrix)
    {
        double cXMin, cYMin, cZMin;
        double cXMax, cYMax, cZMax;
        cXMin = cXMax = objList[0].XValue;
        cYMin = cYMax = objList[0].YValue;
        cZMin = cZMax = objList[0].ZValue;

        double cW = 1;

        foreach (Vector3D obj in objList)
        {
            if (obj.XValue < cXMin)
                cXMin = obj.XValue;
            if (obj.XValue > cXMax)
                cXMax = obj.XValue;
            if (obj.YValue < cYMin)
                cYMin = obj.YValue;
            if (obj.YValue > cYMax)
                cYMax = obj.YValue;
            if (obj.ZValue < cZMin)
                cZMin = obj.ZValue;
            if (obj.ZValue > cZMax)
                cZMax = obj.ZValue;
        }
        Vector3D centerP = new Vector3D(
            (cXMin + cXMax) / 2,
            (cYMin + cYMax) / 2,
            (cZMin + cZMax) / 2,
            cW);

        Console.WriteLine("calculated Center Point: <" + centerP.XValue + ", "
                + centerP.YValue + ", " + centerP.ZValue + ">.\n");
        //to be listed as columns
        List<Vector3D> scaledObj = new List<Vector3D>();

        //Listed as rows
        List<Vector3D> sMatrix = new List<Vector3D>();
        sMatrix.Add(new Vector3D(scaleMatrix.XValue, 0, 0, centerP.XValue * (1 - scaleMatrix.XValue)));
        sMatrix.Add(new Vector3D(0, scaleMatrix.YValue, 0, centerP.YValue * (1 - scaleMatrix.YValue)));
        sMatrix.Add(new Vector3D(0, 0, scaleMatrix.ZValue, centerP.ZValue * (1 - scaleMatrix.ZValue)));
        sMatrix.Add(new Vector3D(0, 0, 0, centerP.WValue));

        foreach (Vector3D column in objList)
        {
            Vector3D temp = new Vector3D(
                dotProduct4D(sMatrix[0], column),
                dotProduct4D(sMatrix[1], column),
                dotProduct4D(sMatrix[2], column),
                dotProduct4D(sMatrix[3], column));
            scaledObj.Add(temp);
        }

        return scaledObj;
    }

    public static double D2R(double degrees)
    {
        return degrees * Math.PI / (double)180;
    }

    public static double R2D(double radians)
    {
        return radians * (double)180 / Math.PI;
    }

    public static List<Vector3D> RotateAroundXAxis(List<Vector3D> objList, double theta)
    {
        //to be listed as columns
        List<Vector3D> rotatedObj = new List<Vector3D>();



        //Listed as rows
        List<Vector3D> sMatrix = new List<Vector3D>();
        sMatrix.Add(new Vector3D(1, 0, 0, 0));
        sMatrix.Add(new Vector3D(0, Math.Cos(D2R(theta)), -Math.Sin(D2R(theta)), 0));
        sMatrix.Add(new Vector3D(0, Math.Sin(D2R(theta)), Math.Cos(D2R(theta)), 0));
        sMatrix.Add(new Vector3D(0, 0, 0, 1));

        foreach (Vector3D column in objList)
        {
            Vector3D temp = new Vector3D(
                dotProduct4D(sMatrix[0], column),
                dotProduct4D(sMatrix[1], column),
                dotProduct4D(sMatrix[2], column),
                dotProduct4D(sMatrix[3], column));
            rotatedObj.Add(temp);
        }

        return rotatedObj;
    }

    public static List<Vector3D> RotateAroundYAxis(List<Vector3D> objList, double theta)
    {
        //to be listed as columns
        List<Vector3D> rotatedObj = new List<Vector3D>();



        //Listed as rows
        List<Vector3D> sMatrix = new List<Vector3D>();
        sMatrix.Add(new Vector3D(Math.Cos(D2R(theta)), 0, Math.Sin(D2R(theta)), 0));
        sMatrix.Add(new Vector3D(0, 1, 0, 0));
        sMatrix.Add(new Vector3D(-Math.Sin(D2R(theta)), 0, Math.Cos(D2R(theta)), 0));
        sMatrix.Add(new Vector3D(0, 0, 0, 1));

        foreach (Vector3D column in objList)
        {
            Vector3D temp = new Vector3D(
                dotProduct4D(sMatrix[0], column),
                dotProduct4D(sMatrix[1], column),
                dotProduct4D(sMatrix[2], column),
                dotProduct4D(sMatrix[3], column));
            rotatedObj.Add(temp);
        }

        return rotatedObj;
    }

    public static List<Vector3D> RotateAroundZAxis(List<Vector3D> objList, double theta)
    {
        //to be listed as columns
        List<Vector3D> rotatedObj = new List<Vector3D>();



        //Listed as rows
        List<Vector3D> sMatrix = new List<Vector3D>();
        sMatrix.Add(new Vector3D(Math.Cos(D2R(theta)), -Math.Sin(D2R(theta)), 0, 0));
        sMatrix.Add(new Vector3D(Math.Sin(D2R(theta)), Math.Cos(D2R(theta)), 0, 0));
        sMatrix.Add(new Vector3D(0, 0, 1, 0));
        sMatrix.Add(new Vector3D(0, 0, 0, 1));

        foreach (Vector3D column in objList)
        {
            Vector3D temp = new Vector3D(
                dotProduct4D(sMatrix[0], column),
                dotProduct4D(sMatrix[1], column),
                dotProduct4D(sMatrix[2], column),
                dotProduct4D(sMatrix[3], column));
            rotatedObj.Add(temp);
        }

        return rotatedObj;
    }
}
