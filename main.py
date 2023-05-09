from Generator import Generator
from utils import set_openai_key

if __name__ == "__main__":
    set_openai_key()
    generator = Generator()

    while True:
        input_str = input("User:")

        if input_str.lower() == "quit" or input_str.lower() == "q":
            break

        print("Generator_bot:" + generator(input_str))

    print("quit success")
