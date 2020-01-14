using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form

    {
        Shape shape1, shape2; //declaration of shapeFactory
        Color c;
        Variables v;
        Point point; //defines points in panel

        string console_text;

        //list to hold individuals objects
        List<Circle> circleObjects;
        List<Rectangle> rectangleObjects;
        List<Line> lineObjects;
        List<Polygon> polygonObjects;
        List<Variables> variableObjects;
        List<MoveDirection> moveObjects;

        Circle circle; //declaration of circle
        Rectangle rectangle; //declaration of rectangle
        Line line;
        Boolean CreateCircle, DrawRectangle, MoveCursor, drawtriangle, drawLine; //boolean to check whether to make objects or not
        String program; //string to hold textarea info
        String[] words; //words of the individual program 
        int moveX, moveY; //cursor moving direction points
        int counter; // counter to loop code
        int thickness; //thickness of pen
        int loopCounter; //loopcounter to hold loop value in loop code

        public Form1()
        {
            InitializeComponent();
            //Shape creation and initialization
            AbstractFactory shapeFactory = FactoryProducer.getFactory("Shape");
            shape1 = shapeFactory.getShape("Circle");
            shape2 = shapeFactory.getShape("Rectangle");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            circleObjects.Clear();
            rectangleObjects.Clear();
            moveObjects.Clear();
            variableObjects.Clear();
            polygonObjects.Clear();
            lineObjects.Clear();
            this.CreateCircle = false;
            this.DrawRectangle = false;
            this.MoveCursor = false;
            this.MultipleLines.Clear();
            this.Singleline.Clear();
            this.drawtriangle = false;
            Panel.Refresh();
            moveX = 0;
            moveY = 0;

        }

        private void MRun_Click(object sender, EventArgs e)
        {
            try
            {
                //whole written program
                program = MultipleLines.Text;


                // ... Parts are separated by Windows line breaks.

                // Use a char array of 2 characters (\r and \n).
                // ... Break lines into separate strings.
                // ... Use RemoveEntryEntries so empty strings are not added.
                char[] delimiters = new char[] { '\r', '\n' };
                string[] parts = program.Split(delimiters, StringSplitOptions.RemoveEmptyEntries); //holds invididuals code line
                console_text = Environment.NewLine + "Program code: \n";
                foreach (string part in parts)
                {
                    console_text += Environment.NewLine + part + "\n";
                }
                console_text += Environment.NewLine + "";


                //loop through the whole program code line
                for (int i = 0; i < parts.Length; i++)
                {

                    //single code line
                    String code_line = parts[i];

                    char[] code_delimiters = new char[] { ' ' };
                    words = code_line.Split(code_delimiters, StringSplitOptions.RemoveEmptyEntries); //holds invididuals code line

                    //calculation to add value to variable
                    if (Regex.IsMatch(words[0], @"^[a-zA-Z]+$") && words[1].Equals("+"))
                    {

                        //sets new incremented value to the defined variable and puts it in vaiableObjects class
                        variableObjects[variableObjects.FindIndex(x => x.variable.Contains(words[0]))].
                            setValue(variableObjects[variableObjects.FindIndex(x => x.variable.Contains(words[0]))]
                            .getValue() + Convert.ToInt32(words[2]));
                        console_text += "\n Adding:  \n" + words[0] + " + " + words[2] + " = " +
                            variableObjects[variableObjects.FindIndex(x => x.variable.Contains(words[0]))].getValue().ToString() + "\n\n";

                    }


                    //assigns value to defined variables
                    if ((Regex.IsMatch(words[0], @"^[a-zA-Z]+$") && words[1].Equals("=")))
                    {
                        //add new variableObjects if variableObject is empty
                        if (variableObjects == null || variableObjects.Count == 0)
                        {
                            v = new Variables();
                            v.setVariable(words[0]);
                            v.setValue(Convert.ToInt32(words[2]));
                            console_text += "\n Adding variable: \n" + v.getVariable() + " = " + v.getValue().ToString() + "\n\n";
                            variableObjects.Add(v);
                        }
                        else
                        {
                            //checks if variable and it's value exists in variableObjects or not 
                            if (variableObjects.Exists(x => x.variable == words[0] && x.value == Convert.ToInt32(words[2])) == true)
                            {
                                console_text += "\n variable exists: \n" + words[0] + " = "
                                    + variableObjects[variableObjects.FindIndex(x => x.variable.Contains(words[0]))]
                                    .getValue().ToString() + "\n\n";

                            }//else checks if variable exists or not
                            else if (!variableObjects.Exists(x => x.variable == words[0]))
                            {
                                v = new Variables();
                                v.setVariable(words[0]);
                                v.setValue(Convert.ToInt32(words[2]));
                                console_text += "\n Adding variable: \n" + v.getVariable() + " = " + v.getValue().ToString() + "\n\n";
                                variableObjects.Add(v);
                            }
                            //else add new variable value to variableObjects
                            else
                            {
                                v = new Variables();
                                v.setVariable(words[0]);
                                v.setValue(Convert.ToInt32(words[2]));
                                variableObjects[variableObjects.FindIndex(x => x.variable.Contains(words[0]))] = v;
                                console_text += "\n Replacing variable value: \n" + words[0] + " = "
                                    + variableObjects[variableObjects.FindIndex(x => x.variable.Contains(words[0]))]
                                    .getValue().ToString() + "\n\n";

                            }
                        }
                    }

                    //shows variableObjects lists
                    if (words[0] == "Show")
                    {
                        foreach (Variables v in variableObjects)
                        {
                            MessageBox.Show("Full variable: " + v.getVariable());
                            MessageBox.Show("Full value: " + Convert.ToString(v.getValue()));
                        }
                    }

                    //condition to check if "draw" then
                    if (words[0].Contains("Draw") == true || words[0].Contains("draw") == true)
                    {
                        counter += 1;//value to increment draw circle method

                        if (words[1].Contains("Circle") == true || words[1].Contains("circle") == true)
                        // condition to check if "circle" then
                        {
                            if (!(words.Length == 3)) //checks if written code is correct or not
                            {
                                MessageBox.Show("!!Please enter correct command!!");
                                console_text += Environment.NewLine + " Correct code be like: \n e.g. draw circle 100 or draw circle r \n\n";
                            }
                            else
                            {
                                if (variableObjects.Exists(x => x.variable == words[2]) == true)
                                //assigns variable value to draw code parameter value
                                {
                                    words[2] = Convert.ToString(variableObjects.ElementAt(variableObjects.
                                        FindIndex(x => x.variable.Contains(words[2]))).getValue()); //variable value to radius parameter
                                }
                                if (circleObjects.Exists(x => x.getX() == moveX && x.getY() == moveY
                                && x.getRadius() == Convert.ToInt32(words[2])) == true)
                                //checks if circle with x,y,radius parameter exists or not
                                {
                                    console_text += Environment.NewLine + " !!circle object exists with given parameters!!\n\n";

                                }
                                else
                                {//if not exists then creates new circle and add to circleObjects and draws circle
                                    Circle circle = new Circle();
                                    circle.setX(moveX);
                                    circle.setY(moveY);
                                    circle.setRadius(Convert.ToInt32(words[2]));
                                    circleObjects.Add(circle);
                                    CreateCircle = true;
                                    console_text += Environment.NewLine + " Adding new circle\n\n";
                                }
                            }
                        }





                        if (words[1].Contains("Rectangle") == true || words[1].Contains("rectangle") == true)
                        {
                            //MessageBox.Show(moveX.ToString());
                            if (!(words.Length == 4)) //extending parameter values
                            {
                                MessageBox.Show("!!Please enter correct command!!");
                                console_text += Environment.NewLine + " Correct code be like: \n e.g. draw rectangle 100 100 or draw circle h w \n\n";
                            }
                            else
                            {

                                //assigns variable value to draw code parameter value
                                if (variableObjects.Exists(x => x.variable == words[2] == true))
                                {
                                    words[2] = Convert.ToString(variableObjects.ElementAt(variableObjects.
                                        FindIndex(x => x.variable.Contains(words[2]))).getValue()); //variable value to height parameter
                                }
                                if (variableObjects.Exists(x => x.variable == words[3]) == true)
                                {
                                    words[3] = Convert.ToString(variableObjects.ElementAt(variableObjects.
                                        FindIndex(x => x.variable.Contains(words[3]))).getValue()); //variable value to width parameter
                                }


                                if (rectangleObjects.Exists(x => x.getX() == moveX && x.getY() == moveY
                                && x.getHeight() == Convert.ToInt32(words[2]) && x.getWidth() ==
                                Convert.ToInt32(words[3])) == true)//checks if rectangle with x,y,height,width parameter exists or not
                                {
                                    console_text += Environment.NewLine + " !!rectangle object exists with given parameters!!\n\n";
                                }
                                else
                                {//if not exists then creates new rectangle and add to rectangleObjects and draws rectangle
                                    Rectangle rect = new Rectangle();
                                    rect.setX(moveX);
                                    rect.setY(moveY);
                                    rect.setHeight(Convert.ToInt32(words[2]));
                                    rect.setWidth(Convert.ToInt32(words[3]));
                                    rectangleObjects.Add(rect);
                                    DrawRectangle = true;
                                    console_text += Environment.NewLine + " Adding new rectangle \n\n";
                                }
                            }
                        }



                        if (words[1].Contains("Polygon") == true || words[1].Contains("triangle") == true || words[1].Contains("Triangle")==true || words[1].Contains("polygon") == true)

                        {

                            drawtriangle = true;

                        }
                    }

                    if (words[0].Contains("DrawTo") == true || words[0].Contains("drawto") == true)
                    {
                        if (!(words.Length == 3)) //checks if written code is correct or not
                        {
                            MessageBox.Show("!!Please enter correct command!!");
                            console_text += Environment.NewLine + "Correct code be like: \n e.g. drawTo 100 100 \n\n";
                        }
                        else
                        {
                            Line line = new Line();
                            //line.setX1(pbOutput.Location.X);
                            //line.setY1(pbOutput.Location.Y);
                            line.setX2(Convert.ToInt32(words[1]));
                            line.setY2(Convert.ToInt32(words[2]));
                            lineObjects.Add(line);
                            drawLine = true;


                            moveX = Convert.ToInt32(words[1]);
                            moveY = Convert.ToInt32(words[2]);
                            point = new Point();
                            point.X = moveX;
                            point.Y = moveY;


                            console_text += Environment.NewLine + " Adding new circle\n\n";
                        }
                    }

                    if (words[0].Contains("MoveTo") == true || words[0].Contains("moveto") == true) // condition to check if "move" then
                    {
                        moveX = Convert.ToInt32(words[1]);
                        moveY = Convert.ToInt32(words[2]);
                        MoveCursor = true;
                        console_text += Environment.NewLine + "X=" + moveX + Environment.NewLine  + "Y=" + moveY + "\n\n";
                    }

                    if (words[0] == "color")
                    {
                        thickness = Convert.ToInt32(words[2]);

                        if (words[1] == "red")
                        {
                            c = Color.Red;
                            console_text += Environment.NewLine + "Pen is of red color\n\n";
                        }
                        else if (words[1] == "blue")
                        {
                            c = Color.Blue;
                            console_text += Environment.NewLine + "Pen is of blue color\n\n";
                        }
                        else if (words[1] == "yellow")
                        {
                            c = Color.Yellow;
                            console_text += Environment.NewLine + "Pen is of yellow color\n\n";
                        }
                        else
                        {
                            c = Color.Green;
                            console_text += Environment.NewLine + "Pen is of green color\n\n";
                        }
                    }




                    if (words[0] == "if") //code for if statement
                    {
                        string variable_name = words[1];
                        int value = Convert.ToInt32(words[3]);
                        if (variableObjects.Exists(x => x.variable == words[1]) == true
                            && variableObjects.Exists(x => x.value == Convert.ToInt32(words[3])) == true)
                        //checks if condition defined in if condition matches with variable objects list
                        {
                            console_text += Environment.NewLine + "Entered into if statement\n\n";

                        }
                        else
                        {//directed to end if line
                            i = Array.IndexOf(parts, "end if");
                        }

                    }



                    if (words[0] == "loop") //code for loop statement
                    {
                        loopCounter = Convert.ToInt32(words[1]); //defines loop counter variable
                        console_text += Environment.NewLine + "Entered into loop statement\n\n";
                    }



                    if (parts[i] == "end loop") // code for end loop statement
                    {
                        if (counter < loopCounter) //if counter to draw is not less than loop counter
                        {
                            i = Array.IndexOf(parts, "loop " + loopCounter);
                        }
                        else // keep drawing
                        {
                            i = Array.IndexOf(parts, "end loop");
                        }
                    }
                }

            }
            catch (IndexOutOfRangeException ex)
            {
                console_text += Environment.NewLine + "Error: " + ex.Message + "\n\n";
            }
            catch (FormatException ex)
            {
                console_text += Environment.NewLine + " !!Please input correct parameter!!\n\n";
            }
            catch (ArgumentOutOfRangeException ex)
            {
                console_text += Environment.NewLine + "!!Please input correct parameter!!\n\n";

            }
            Console.Text = console_text;
            Panel.Refresh(); //refresh with every drawing equals to true
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            //Graphics to draw in panel
            Graphics g = e.Graphics;

            if (CreateCircle == true)//draw circle condition
            {
                foreach (Circle circleObject in circleObjects)
                {
                    console_text += Environment.NewLine + "Drawing Circle\n\n";
                    circleObject.draw(g, c, thickness); //draw circle with given graphics
                }
            }

            if (DrawRectangle == true) //draw rectangle condition
            {
                foreach (Rectangle rectangleObject in rectangleObjects)
                {
                    console_text += Environment.NewLine + "Drawing Rectangle\n\n";
                    rectangleObject.draw(g, c, thickness); //draw circle with given graphics
                }
            }

            if (MoveCursor == true) //condition to move pointer
            {
                console_text += Environment.NewLine + "\n\n";
                point = new Point();
                point.X = moveX;
                point.Y = moveY;

            }

            if (drawLine == true)
            {
                foreach (Line lineObject in lineObjects)
                {
                    console_text += Environment.NewLine + "Drawing Line\n\n";
                    lineObject.draw(g, c, thickness); //draw line with given graphics
                }
            }

            if (drawtriangle == true)
            {
                Pen blackPen = new Pen(Color.BlueViolet, 3);
                PointF point1 = new PointF(300.0F, 50.0F);
                PointF point2 = new PointF(400.0F, 25.0F);
                PointF point3 = new PointF(201.0F, 8.0F);
                
                string[] str = new string[3];
                PointF[] curvePoints =
                 {
                point1,
                 point2,
                 point3,
                 
             };
                e.Graphics.DrawPolygon(blackPen, curvePoints);
            }
            Console.Text = console_text;
        
    }

        private void Clear_Click(object sender, EventArgs e)
        {
            MultipleLines.Clear();
            Singleline.Clear();
            Panel.Invalidate();
            circleObjects.Clear();
            rectangleObjects.Clear();
            /*variableObjects.Clear();*/
            polygonObjects.Clear();
            lineObjects.Clear();
            this.CreateCircle = false;
            this.DrawRectangle = false;
            this.MoveCursor = false;
            this.drawtriangle = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //whole written program
                program = Singleline.Text;


                // ... Parts are separated by Windows line breaks.

                // Use a char array of 2 characters (\r and \n).
                // ... Break lines into separate strings.
                // ... Use RemoveEntryEntries so empty strings are not added.
                char[] delimiters = new char[] { '\r', '\n' };
                string[] parts = program.Split(delimiters, StringSplitOptions.RemoveEmptyEntries); //holds invididuals code line
                console_text = "Program code: \n";
                foreach (string part in parts)
                {
                    console_text += part + "\n";
                }
                console_text += Environment.NewLine + "\n\n";


                //loop through the whole program code line
                for (int i = 0; i < parts.Length; i++)
                {

                    //single code line
                    String code_line = parts[i];

                    char[] code_delimiters = new char[] { ' ' };
                    words = code_line.Split(code_delimiters, StringSplitOptions.RemoveEmptyEntries); //holds invididuals code line

                    //calculation to add value to variable
                    if (Regex.IsMatch(words[0], @"^[a-zA-Z]+$") && words[1].Equals("+"))
                    {

                        //sets new incremented value to the defined variable and puts it in vaiableObjects class
                        variableObjects[variableObjects.FindIndex(x => x.variable.Contains(words[0]))].
                            setValue(variableObjects[variableObjects.FindIndex(x => x.variable.Contains(words[0]))]
                            .getValue() + Convert.ToInt32(words[2]));
                        console_text += Environment.NewLine + "\n Adding:  \n" + words[0] + " + " + words[2] + " = " +
                            variableObjects[variableObjects.FindIndex(x => x.variable.Contains(words[0]))].getValue().ToString() + "\n\n";

                    }


                    //assigns value to defined variables
                    if ((Regex.IsMatch(words[0], @"^[a-zA-Z]+$") && words[1].Equals("=")))
                    {
                        //add new variableObjects if variableObject is empty
                        if (variableObjects == null || variableObjects.Count == 0)
                        {
                            v = new Variables();
                            v.setVariable(words[0]);
                            v.setValue(Convert.ToInt32(words[2]));
                            console_text += Environment.NewLine + "\n Adding variable: \n" + v.getVariable() + " = " + v.getValue().ToString() + "\n\n";
                            variableObjects.Add(v);
                        }
                        else
                        {
                            //checks if variable and it's value exists in variableObjects or not 
                            if (variableObjects.Exists(x => x.variable == words[0] && x.value == Convert.ToInt32(words[2])) == true)
                            {
                                console_text += Environment.NewLine + "\n variable exists: \n" + words[0] + " = "
                                    + variableObjects[variableObjects.FindIndex(x => x.variable.Contains(words[0]))]
                                    .getValue().ToString() + "\n\n";

                            }//else checks if variable exists or not
                            else if (!variableObjects.Exists(x => x.variable == words[0]))
                            {
                                v = new Variables();
                                v.setVariable(words[0]);
                                v.setValue(Convert.ToInt32(words[2]));
                                console_text += Environment.NewLine + "\n Adding variable: \n" + v.getVariable() + " = " + v.getValue().ToString() + "\n\n";
                                variableObjects.Add(v);
                            }
                            //else add new variable value to variableObjects
                            else
                            {
                                v = new Variables();
                                v.setVariable(words[0]);
                                v.setValue(Convert.ToInt32(words[2]));
                                variableObjects[variableObjects.FindIndex(x => x.variable.Contains(words[0]))] = v;
                                console_text += Environment.NewLine + "\n Replacing variable value: \n" + words[0] + " = "
                                    + variableObjects[variableObjects.FindIndex(x => x.variable.Contains(words[0]))]
                                    .getValue().ToString() + "\n\n";

                            }
                        }
                    }

                    //shows variableObjects lists
                    if (words[0] == "Show")
                    {
                        foreach (Variables v in variableObjects)
                        {
                            MessageBox.Show("Full variable: " + v.getVariable());
                            MessageBox.Show("Full value: " + Convert.ToString(v.getValue()));
                        }
                    }

                    //condition to check if "draw" then
                    if (words[0].Contains("Draw") == true || words[0].Contains("draw") == true)
                    {
                        counter += 1;//value to increment draw circle method

                        if (words[1].Contains("Circle") == true || words[1].Contains("circle") == true)
                        // condition to check if "circle" then
                        {
                            if (!(words.Length == 3)) //checks if written code is correct or not
                            {
                                MessageBox.Show("!!Please enter correct command!!");
                                console_text += Environment.NewLine + "\n Correct code be like: \n e.g. draw circle 100 or draw circle r \n\n";
                            }
                            else
                            {
                                if (variableObjects.Exists(x => x.variable == words[2]) == true)
                                //assigns variable value to draw code parameter value
                                {
                                    words[2] = Convert.ToString(variableObjects.ElementAt(variableObjects.
                                        FindIndex(x => x.variable.Contains(words[2]))).getValue()); //variable value to radius parameter
                                }
                                if (circleObjects.Exists(x => x.getX() == moveX && x.getY() == moveY
                                && x.getRadius() == Convert.ToInt32(words[2])) == true)
                                //checks if circle with x,y,radius parameter exists or not
                                {
                                    console_text += Environment.NewLine + "\n !!circle object exists with given parameters!!\n\n";

                                }
                                else
                                {//if not exists then creates new circle and add to circleObjects and draws circle
                                    Circle circle = new Circle();
                                    circle.setX(moveX);
                                    circle.setY(moveY);
                                    circle.setRadius(Convert.ToInt32(words[2]));
                                    circleObjects.Add(circle);
                                    CreateCircle = true;
                                    console_text += Environment.NewLine + "\n Adding new circle\n\n";
                                }
                            }
                        }





                        if (words[1].Contains("Rectangle") == true || words[1].Contains("rectangle") == true)
                        {
                            //MessageBox.Show(moveX.ToString());
                            if (!(words.Length == 4)) //extending parameter values
                            {
                                MessageBox.Show("!!Please enter correct command!!");
                                console_text += Environment.NewLine + "\n Correct code be like: \n e.g. draw rectangle 100 100 or draw circle h w \n\n";
                            }
                            else
                            {

                                //assigns variable value to draw code parameter value
                                if (variableObjects.Exists(x => x.variable == words[2] == true))
                                {
                                    words[2] = Convert.ToString(variableObjects.ElementAt(variableObjects.
                                        FindIndex(x => x.variable.Contains(words[2]))).getValue()); //variable value to height parameter
                                }
                                if (variableObjects.Exists(x => x.variable == words[3]) == true)
                                {
                                    words[3] = Convert.ToString(variableObjects.ElementAt(variableObjects.
                                        FindIndex(x => x.variable.Contains(words[3]))).getValue()); //variable value to width parameter
                                }


                                if (rectangleObjects.Exists(x => x.getX() == moveX && x.getY() == moveY
                                && x.getHeight() == Convert.ToInt32(words[2]) && x.getWidth() ==
                                Convert.ToInt32(words[3])) == true)//checks if rectangle with x,y,height,width parameter exists or not
                                {
                                    console_text += Environment.NewLine + "\n !!rectangle object exists with given parameters!!\n\n";
                                }
                                else
                                {//if not exists then creates new rectangle and add to rectangleObjects and draws rectangle
                                    Rectangle rect = new Rectangle();
                                    rect.setX(moveX);
                                    rect.setY(moveY);
                                    rect.setHeight(Convert.ToInt32(words[2]));
                                    rect.setWidth(Convert.ToInt32(words[3]));
                                    rectangleObjects.Add(rect);
                                    DrawRectangle = true;
                                    console_text += Environment.NewLine + "\n Adding new rectangle\n\n";
                                }
                            }
                        }



                        if (words[1].Contains("Polygon") == true || words[1].Contains("triangle") == true || words[1].Contains("Triangle") == true || words[1].Contains("polygon") == true)
                        {

                            drawtriangle = true;

                        }
                       
                    }

                    if (words[0].Contains("DrawTo") == true || words[0].Contains("drawto") == true)
                    {
                        if (!(words.Length == 3)) //checks if written code is correct or not
                        {
                            MessageBox.Show("!!Please enter correct command!!");
                            console_text += Environment.NewLine + "\n Correct code be like: \n e.g. drawTo 100 100 \n\n";
                        }
                        else
                        {
                            Line line = new Line();
                            //line.setX1(pbOutput.Location.X);
                            //line.setY1(pbOutput.Location.Y);
                            line.setX2(Convert.ToInt32(words[1]));
                            line.setY2(Convert.ToInt32(words[2]));
                            lineObjects.Add(line);
                            drawLine = true;


                            moveX = Convert.ToInt32(words[1]);
                            moveY = Convert.ToInt32(words[2]);
                            point = new Point();
                            point.X = moveX;
                            point.Y = moveY;


                            console_text += Environment.NewLine + "\n Adding new circle\n\n";
                        }
                    }

                    if (words[0].Contains("MoveTo") == true || words[0].Contains("moveto") == true) // condition to check if "move" then
                    {
                        moveX = Convert.ToInt32(words[1]);
                        moveY = Convert.ToInt32(words[2]);
                        MoveCursor = true;
                        console_text += Environment.NewLine + "X=" + moveX + "\n" + "Y=" + moveY + "\n\n";
                    }

                    if (words[1] == "color")
                    {
                        thickness = Convert.ToInt32(words[2]);

                        if (words[1] == "red")
                        {
                            c = Color.Red;
                            console_text += Environment.NewLine + " \n Pen is of red color\n\n";
                        }
                        else if (words[1] == "blue")
                        {
                            c = Color.Blue;
                            console_text += Environment.NewLine + "\n Pen is of blue color\n\n";
                        }
                        else if (words[1] == "yellow")
                        {
                            c = Color.Yellow;
                            console_text += Environment.NewLine + "\n Pen is of yellow color\n\n";
                        }
                        else
                        {
                            c = Color.Green;
                            console_text += Environment.NewLine + "\n Pen is of green color\n\n";
                        }
                    }




                    if (words[0] == "if") //code for if statement
                    {
                        string variable_name = words[1];
                        int value = Convert.ToInt32(words[3]);
                        if (variableObjects.Exists(x => x.variable == words[1]) == true
                            && variableObjects.Exists(x => x.value == Convert.ToInt32(words[3])) == true)
                        //checks if condition defined in if condition matches with variable objects list
                        {
                            console_text += Environment.NewLine + "\n Entered into if statement\n\n";

                        }
                        else
                        {//directed to end if line
                            i = Array.IndexOf(parts, "end if");
                        }

                    }



                    if (words[0] == "loop") //code for loop statement
                    {
                        loopCounter = Convert.ToInt32(words[1]); //defines loop counter variable
                        console_text += Environment.NewLine + "\n Entered into loop statement\n\n";
                    }



                    if (parts[i] == "end loop") // code for end loop statement
                    {
                        if (counter < loopCounter) //if counter to draw is not less than loop counter
                        {
                            i = Array.IndexOf(parts, "loop" + loopCounter);
                        }
                        else // keep drawing
                        {
                            i = Array.IndexOf(parts, "end loop");
                        }
                    }
                }

            }
            catch (IndexOutOfRangeException ex)
            {
                console_text += Environment.NewLine + "\n Error: " + ex.Message + "\n\n";
            }
            catch (FormatException ex)
            {
                console_text += Environment.NewLine + "\n !!Please input correct parameter!!\n\n";
            }
            catch (ArgumentOutOfRangeException ex)
            {
                console_text += Environment.NewLine + "\n !!Please input correct parameter!!\n\n";

            }
            Console.Text = console_text;
            Panel.Refresh(); //refresh with every drawing equals to true
        }

        private void insturctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("For drawing without parameter:\n" +
                            "\n draw circle 100 \n draw rectangle 100 100 \n \n" +
                            "For drawing with parameter: \n r = 100 \n draw circle r \n h = 100 \n w = 100 \n draw rectangle h w \n \n" +
                            "For moving cursor: \n moveto 100 100 \n \n" +
                            "For drawing line: \n drawto 100 100\n \n" +
                            "For choosing color: \n color = red \n \n" +
                            "For declaring variable: \n counter = 100 \n \n" +
                            "For looping: \n r = 100 \n loop 4 \n r + 100 \n draw circle r \n end loop \n \n " +
                            "For if statement: \n counter = 5 \n if counter = 5 then \n draw circle 100 \n end if \n \n");
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;//instantiating null value for stra
            OpenFileDialog openFileDialog1 = new OpenFileDialog();//creating a instance of a dialog box
            openFileDialog1.InitialDirectory = "D:\\TBC\\Semester 7 & 8\\ASE\\Workspace\\SoftwareAssignment\\WindowsFormsApp1";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; //creting text form
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)//if choose data is fine then enter todialog box
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null) //if the choosen file is not empty then the file is read
                    {
                        using (myStream) //dispose is done automatically
                        {
                            MultipleLines.Text = File.ReadAllText(openFileDialog1.FileName);//loading text file in multiple Line
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private async void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();//creating a instance of a dialog box
            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; //creting text form
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)//if choose data is fine then enter todialog box
            {
                try
                {

                    using (StreamWriter write = new StreamWriter(sfd.FileName)) //dispose is done automatically
                    {
                        await write.WriteLineAsync(MultipleLines.Text);//loading text file in multipleLine
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            circle = new Circle(); //creates new circle
            rectangle = new Rectangle(); //creates new rectangle
            circleObjects = new List<Circle>(); //creates array of new circle objects
            rectangleObjects = new List<Rectangle>(); //creates array of new rectangle objects
            lineObjects = new List<Line>();
            variableObjects = new List<Variables>(); //creates array of new variable objects
            moveObjects = new List<MoveDirection>(); //creates array of new move objects
            polygonObjects = new List<Polygon>();

            c = Color.DarkRed;

        }
        
        

    }
}
