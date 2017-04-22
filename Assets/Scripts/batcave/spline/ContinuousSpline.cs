using UnityEngine;

namespace BatCave.Spline {
public class ContinuousSpline : SingleSpline {
    public ContinuousSpline(Vector2[] firstControlPoints) : base(firstControlPoints) {
        // Empty on purpose.
    }

    /// <summary>
    /// Continue the spline with more control points.
    /// </summary>
    public void AddControlPoints(params Vector2[] points) {
        // TODO: Implement: don't keep all of the control points, there could be
        //       millions of them!
    
			controlPointscopy [0] = controlPointscopy [controlPointscopy.Length - 2];
			controlPointscopy [1] = controlPointscopy [controlPointscopy.Length - 1];
			for (int i = 2; i < controlPointscopy.Length; i++) {
				controlPointscopy [i] = points [i - 2];
			}

			for (int i = 0; i < num_of_points ; i++) {
				if (i == 0 ) {
					b_s [0] = 1;
					c_s [0] = 0;
					d_s [0] = (3*(controlPointscopy [i + 1].y - controlPointscopy [i].y)) /
						Mathf.Pow((controlPointscopy [i + 1].x - controlPointscopy [i].x),2);
					c_tags [0] = c_s [0] / b_s [0];
					d_tags [0] = d_s [0] / b_s [0];

				}
				else if (i == num_of_points -1) {
					a_s [i] = 1 / (controlPointscopy [i].x - controlPointscopy [i-1].x );
					b_s [i] = 2 / (controlPointscopy [i] .x - controlPointscopy [i-1].x );
					d_s [i] = 3*((controlPointscopy [i].y - controlPointscopy [i-1].y) /
						Mathf.Pow((controlPointscopy [i].x - controlPointscopy [i-1].x),2));
					//c_tags [i] = c_s [i] / (b_s [i] - a_s [i]);
					d_tags [i] = (d_s [i] - a_s [i] * d_tags [i - 1])/
						(b_s [i] - a_s [i]*c_tags [i-1]) ;


				}
				else{
					a_s [i] = 1 / (controlPointscopy [i].x  - controlPointscopy [i-1].x );
					b_s [i] = 2*((1 / (controlPointscopy [i].x  - controlPointscopy [i-1].x ))+
						(1 / (controlPointscopy [i + 1].x  - controlPointscopy [i].x )));

					d_s [i] = 3*(((controlPointscopy [i ].y - controlPointscopy [i-1].y) /
						Mathf.Pow((controlPointscopy [i].x  - controlPointscopy [i-1].x ),2))+
						((controlPointscopy [i+1 ].y - controlPointscopy [i].y) /
							Mathf.Pow((controlPointscopy [i+1].x  - controlPointscopy [i].x ),2)));
					c_s [i] = 1 / (controlPointscopy [i + 1].x  - controlPointscopy [i].x );
					c_tags [i] = c_s [i] / (b_s [i] - a_s [i]);
					d_tags [i] = (d_s [i] - a_s [i] * d_tags [i - 1])/
						(b_s [i] - a_s [i]*c_tags [i-1]);

				}
			}
		}
}
}
