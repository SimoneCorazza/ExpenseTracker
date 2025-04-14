import os
import os.path
import sys

env_js_path = sys.argv[1]
if not os.path.isfile(env_js_path):
    raise Exception(f"The path '{env_js_path}' is not the file.\nI need the env.js file to replace the configuration!")

string = "window.env = {\n"
for key, value in os.environ.items():
    if key.startswith("REACT_APP"):
        string += f"\t{key}: '{value}',\n"
string += "};"

with open(env_js_path, 'w') as json_file:
    json_file.write(string)
