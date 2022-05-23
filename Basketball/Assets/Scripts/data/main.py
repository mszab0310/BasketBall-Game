import matplotlib.pyplot as plt
import numpy as np
import os
import pathlib


def plot_data(file_name):
    with open(file_name, "r") as f:
        data = [list(map(float, line.strip().split(" "))) for line in f]

        means = [np.mean(generation) for generation in data]
        maxes = [np.mean(sorted(g, reverse=True)[:10]) for g in data]

        plt.plot(range(len(means)), means, label="avg")  # TODO display meaningful messages
        plt.plot(range(len(maxes)), maxes, label="top10")
        plt.legend()


if __name__ == '__main__':
    path = pathlib.Path().absolute()
    print(path)
    folder_name = path
    for f_name in filter(lambda x: x.endswith(".txt"), os.listdir(folder_name)):
        plot_data(os.path.join(folder_name, f_name))
    plt.savefig('percentageFitnessK')
    plt.show()
