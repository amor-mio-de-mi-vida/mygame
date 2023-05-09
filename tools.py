import os
from langchain import PromptTemplate, LLMChain
from langchain.agents import tool, Tool

import prompts


@tool("ReadFile")
def read_file(code_file_path):
    """
    useful when you want get the content of the file.
    because of token limitations, you can not use this tool for one file more than one time.
    the input is the path to the source code file. You must enter a real file path.
    the output is all the contents of the file.
    """
    try:
        with open(code_file_path, "r") as f:
            return f.readlines()
    except IOError:
        return (
            "source code file not found, "
            "you should use tool ProjectStructure to get the correct path before you use this one"
        )


class BgRelation:
    def __init__(self, llm):
        prompt = PromptTemplate(
            input_variables=["BG1", "BG2"],
            template=prompts.Bg_Relation_TEMPLATE
        )
        self.chain = LLMChain(llm=llm, prompt=prompt)

    def __call__(self, code_file_path):
        metrics_result = self.chain.run(read_file(code_file_path))
        return metrics_result


def get_tools(llm):
    return [
        Tool("MetricsClass", BgRelation(llm), prompts.Bg_Relation_DESC),
    ]
