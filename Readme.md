# Pixdata 
## Embed ASCII data into a Bitmap of uniform color by adding small differences into RGBA pixel

* Start with original Bitmap of one, uniform color. 
* Get 1's and 0's of ASCII character, as in BIN column of https://www.ascii-code.com/
* Edit bitmaps pixels RGBA values: add 1 or 0.

* One RGBA pixel holds four bit word
* Two pixels embed eight bit ASCII character

- - -

## In code

    // assign first bit (0 or 1) into blue part of the pixel 
    // bitstring = "01010011" 
    datapixel.Blue = (byte)int.Parse(bitstring.Substring(0, 1));

    // ..later on; add pixel of 0's and 1s holding ASCII data into original pixel
    originalpixel.Blue += datapixel;

    // made possible by overloaded '+' operator used in compound assignment
    // Note that Alpha is deducted
    public static BuGeRed operator + (BuGeRed bgr, BuGeRed addme)
    {

        return new BuGeRed(new byte[] { (byte)((byte)bgr.Blue + addme.Blue), (byte)((byte)bgr.Green + addme.Green), 
                            (byte)((byte)bgr.Red + addme.Red), (byte)((byte)bgr.Alpha - addme.Alpha) });
    }

