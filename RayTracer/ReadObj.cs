using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RayTracer
{
    public class ReadObj
    {
        public List<Point> Vertices { get; }
        public List<Vector> Normals { get; set; }
        public Group ObjGroup { get; set; }
        public int IgnoredLines { get; }

        public ReadObj(string path)
        {
            Vertices = new List<Point> { null };
            Normals = new List<Vector> { null };
            ObjGroup = new Group();

            double x;
            double y;
            double z;

            foreach (string readLine in File.ReadLines(path))
            {
                string line = readLine.Replace( "  ", " " ).Trim(' ');
                if (line.Length > 2 )
                {
                    switch ( line.Substring( 0, 2 ) )
                    {
                        case "v ":
                            x = double.Parse( line.Substring( 2 ).Split( ' ' )[0] );
                            y = double.Parse( line.Substring( 2 ).Split( ' ' )[1] );
                            z = double.Parse( line.Substring( 2 ).Split( ' ' )[2] );
                            Vertices.Add( new Point( x, y, z ) );
                            break;
                        case "vn":
                            x = double.Parse( line.Substring( 3 ).Split( ' ' )[0] );
                            y = double.Parse( line.Substring( 3 ).Split( ' ' )[1] );
                            z = double.Parse( line.Substring( 3 ).Split( ' ' )[2] );
                            Normals.Add( new Vector( x, y, z ) );
                            break;
                        case "f ":
                            List<string> vertexStrings = line.Substring( 2 ).Split( ' ' ).ToList();

                            if ( line.Substring( 3 ).Contains( "/" ))
                            {
                                List<string> vertices = new List<string>();
                                List<string> normals = new List<string>();
                                foreach (string s in vertexStrings )
                                {
                                    vertices.Add( s.Split( '/' )[0] );
                                    normals.Add( s.Split( '/' )[2] );
                                }
                                foreach ( SmoothTriangle t in FanTriangulation( this, vertices, normals ) )
                                {
                                    AddChildToGroup( t );
                                }

                            }
                            else
                            {
                                foreach ( Triangle t in FanTriangulation( this, vertexStrings ) )
                                {
                                    AddChildToGroup( t );
                                }
                            }
                            break;
                        case "g ":
                            ObjGroup.AddChild( new Group() );
                            break;
                        case "\"\"":
                            break;
                        default:
                            IgnoredLines++;
                            break;
                    }
                }
            }
        }

        private void AddChildToGroup(Shape t)
        {
            if ( ObjGroup.Children.Count > 0 )
            {
                switch ( ObjGroup.Children[ObjGroup.Children.Count - 1] )
                {
                    case Group g:
                        g.AddChild( t );
                        break;
                    default:
                        ObjGroup.AddChild( t );
                        break;
                }
            }
            ObjGroup.AddChild( t );
        }

        private static List<Triangle> FanTriangulation(ReadObj ro, List<string> vertices)
        {
            List<Triangle> triangles = new List<Triangle>();

            for (int i = 1; i < (vertices.Count - 1); i++ )
            {
                triangles.Add(new Triangle( ro.Vertices[int.Parse( vertices[0] )], 
                                            ro.Vertices[int.Parse( vertices[i] )], 
                                            ro.Vertices[int.Parse( vertices[i + 1] )] ));
            }

            return triangles;
        }

        private static List<SmoothTriangle> FanTriangulation(ReadObj ro, List<string> vertices, List<string> normals)
        {
            List<SmoothTriangle> smoothTriangles = new List<SmoothTriangle>();

            for ( int i = 1; i < ( vertices.Count - 1 ); i++ )
            {
                smoothTriangles.Add( new SmoothTriangle( ro.Vertices[int.Parse( vertices[0] )],
                                            ro.Vertices[int.Parse( vertices[i] )],
                                            ro.Vertices[int.Parse( vertices[i + 1] )],
                                            ro.Normals[int.Parse( normals[0] )],
                                            ro.Normals[int.Parse( normals[i] )],
                                            ro.Normals[int.Parse( normals[i + 1] )] ) );
            }

            return smoothTriangles;
        }
    }
}
