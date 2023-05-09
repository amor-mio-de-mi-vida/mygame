import os.path
import argparse
from configparser import ConfigParser


def set_openai_key():
    cfg = ConfigParser()
    cfg.read("config.ini")
    config = dict(cfg.items("openai"))
    os.environ["OPENAI_API_KEY"] = config["openai_key"]