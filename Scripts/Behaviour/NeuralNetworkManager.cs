using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ALUN
{
    [System.Serializable]
    public class NeuralNetworkManager : MonoBehaviour
    {
        // 神经网络对象
        public NeuralNetwork neuralNetwork;

        // 生物的基础参数
        public CreatureParameters creatureParameters;
        //刚体组件
        private Rigidbody rb;
        // 初始化方法，用于创建神经网络，并设置生物的基础参数
        public void Initialize(CreatureParameters parameters)
        {
            if (rb == null) rb = GetComponent<Rigidbody>();
            creatureParameters = parameters;

            // 创建神经网络，设置神经网络的层数、每层的神经元数等参数
            neuralNetwork = new NeuralNetwork(
                parameters.creatureNeuralInfo.inputCount,
                parameters.creatureNeuralInfo.hiddenLayerCount,
                parameters.creatureNeuralInfo.neuronPerHiddenLayer,
                parameters.creatureNeuralInfo.outputCount);
        }


        // 更新方法，用于更新神经网络的输入，并根据神经网络的输出来控制生物的行为
        private void Update()
        {
            // 根据神经网络的输出来控制生物的行为
            ControlCreature(GetOutputs());
        }

        private float[] GetInputs()
        {

            float[] inputs = new float[creatureParameters.creatureNeuralInfo.inputCount];

            // 获取生物的位置
            Vector3 position = transform.position + Vector3.up * 0.5f;
            // 获取最近的障碍物位置
            Vector3[] rayDirections = new Vector3[] {
                transform.forward, // 前方
                -transform.forward,//后方
                -transform.right, // 左侧
                transform.right, // 右侧
                Quaternion.Euler(0, 45, 0) * transform.forward, // 前右方
                Quaternion.Euler(0, -45, 0) * transform.forward, // 前左方
                Quaternion.Euler(0, 135, 0) * transform.forward, // 后左方
                Quaternion.Euler(0, -135, 0) * transform.forward // 后右方
            };  // 设定射线方向，包括前、后、左、右及其对角线 


            //障碍物射线检测结果
            float[] obstacleDistances = new float[rayDirections.Length];  // 初始化一个障碍物数组来存储每个方向的射线距离
            for (int i = 0; i < rayDirections.Length; i++)
            {
                RaycastHit hit = Creature.RaycastForObstacle(position, rayDirections[i], creatureParameters.creatureNeuralInfo.obstacleRayDistance);  // 初始化一个RaycastHit变量，用来存储射线碰撞的信息
                obstacleDistances[i] = hit.collider != null ? hit.distance : creatureParameters.creatureNeuralInfo.obstacleRayDistance;  // 如果射线碰撞了，存储碰撞距离，否则存储最大距离
                if (hit.collider != null)
                    Debug.DrawRay(position, rayDirections[i] * obstacleDistances[i], Color.red);  // 在场景中绘制射线，如果碰撞，射线颜色为红色
            }

            //食物射线检测结果
            float[] foodDistances = new float[rayDirections.Length];  // 初始化一个食物数组来存储每个方向的射线距离
            float[] foodNutrition = new float[rayDirections.Length];  // 初始化一个食物数组来存储每个方向的食物营养值
            for (int i = 0; i < rayDirections.Length; i++)
            {
                RaycastHit hit;
                IGrowable food = Creature.RaycastForFood(position, rayDirections[i], creatureParameters.creatureNeuralInfo.foodRayDistance, out hit);  // 初始化一个RaycastHit变量，用来存储射线碰撞的信息
                foodDistances[i] = food != null ? hit.distance : creatureParameters.creatureNeuralInfo.foodRayDistance;  // 如果射线碰撞了，存储碰撞距离，否则存储最大距离
                foodNutrition[i] = food != null ? food.GetNutrition() : 0f;  // 如果射线碰撞了，存储碰撞距离，否则存储最大距离
                if (food != null)
                    Debug.DrawRay(position + Vector3.up * 0.3f, rayDirections[i] * foodDistances[i], Color.red);  // 在场景中绘制射线，如果碰撞，射线颜色为红色
            }
            float speed = rb.velocity.magnitude / creatureParameters.creatureGameInfo.moveSpeed;
            float rotateSpeed = rb.angularVelocity.magnitude / creatureParameters.creatureGameInfo.rotationSpeed;
            float velocityAngle = Vector3.Angle(rb.velocity, Vector3.right);
            inputs[0] = speed;
            inputs[1] = rotateSpeed;
            inputs[2] = velocityAngle / 180f;
            //将下面3个for的i值都增加1，因为前面已经有3个inputs了
            for (int i = 3; i < 11; i++)
            {
                inputs[i] = obstacleDistances[i - 3] / creatureParameters.creatureNeuralInfo.obstacleRayDistance;
            }
            for (int i = 11; i < 19; i++)
            {
                inputs[i] = foodDistances[i - 11] / creatureParameters.creatureNeuralInfo.foodRayDistance;
            }
            for (int i = 19; i < 27; i++)
            {
                inputs[i] = foodNutrition[i - 19] / 100f;
            }

            //有多少inputs?
            //计算得出inputs数量为27

            return inputs;
        }


        private float[] GetOutputs()
        {
            // 获取生物的输入（例如周围的环境信息）
            float[] inputs = GetInputs();

            // 将输入数据传入神经网络，并获取输出
            float[] outputs = neuralNetwork.FeedForward(inputs);
            return outputs;
        }

        private void ControlCreature(float[] outputs)
        {
            // 解析神经网络的输出
            float moveX = outputs[0] * 300f;
            float moveZ = outputs[1] * 300f;
            float rotateY = outputs[2] * 100f;  // 新增，代表旋转的方向
            float moveY = Mathf.Abs(outputs[3]) * 300f;  // 新增，代表上下移动
            // 创建移动方向向量
            Vector3 moveDirection = new Vector3(moveX, moveY, moveZ);

            // 移动生物
            CreatureTomato creatureTomato = GetComponent<CreatureTomato>();  // 获取具体的 CreatureTomato 实例
            creatureTomato.Tick();
            creatureTomato.Move(moveDirection);

            // 旋转生物
            creatureTomato.Rotate(rotateY);
        }
    }
    [System.Serializable]
    public class NeuralNetwork
    {
        // 我们的神经网络包含多个层，每个层都包含多个神经元
        public List<NeuronLayer> layers = new List<NeuronLayer>();

        public NeuralNetwork(int inputCount, int hiddenLayerCount, int neuronPerHiddenLayer, int outputCount)
        {
            // 创建输入层
            layers.Add(new NeuronLayer(neuronPerHiddenLayer, inputCount));

            // 创建隐藏层
            for (int i = 0; i < hiddenLayerCount - 1; i++)
            {
                layers.Add(new NeuronLayer(neuronPerHiddenLayer, neuronPerHiddenLayer));
            }

            // 创建输出层
            layers.Add(new NeuronLayer(outputCount, neuronPerHiddenLayer));
        }

        public float[] FeedForward(float[] inputs)
        {
            // 将输入数据传给第一层
            layers[0].FeedForward(inputs);

            // 将每一层的输出作为下一层的输入
            for (int i = 1; i < layers.Count; i++)
            {
                layers[i].FeedForward(layers[i - 1].outputs);
            }

            // 返回最后一层的输出
            return layers[layers.Count - 1].outputs;
        }
        public NeuralNetwork Clone()
        {
            NeuralNetwork clone = new NeuralNetwork(0, 0, 0, 0);
            for (int i = 0; i < layers.Count; i++)
            {
                if (i < clone.layers.Count)
                {
                    clone.layers[i] = layers[i].Clone();
                }
                else
                {
                    clone.layers.Add(layers[i].Clone());
                }
            }
            // foreach (NeuronLayer layer in layers)
            // {
            //     clone.layers.Add(layer.Clone());
            // }

            return clone;
        }

        public void Mutate(float mutationRate)
        {
            foreach (NeuronLayer layer in layers)
            {
                layer.Mutate(mutationRate);
            }
        }

        public NeuralNetwork Crossover(NeuralNetwork other)
        {
            NeuralNetwork child = new NeuralNetwork(0, 0, 0, 0);
            for (int i = 0; i < layers.Count; i++)
            {
                child.layers.Add(layers[i].Crossover(other.layers[i]));
            }
            return child;
        }
    }
    [System.Serializable]
    public class NeuronLayer
    {
        // 本层的神经元列表
        public List<Neuron> neurons = new List<Neuron>();

        // 本层的输出值
        public float[] outputs;

        public NeuronLayer(int neuronCount, int inputCountPerNeuron)
        {
            // 创建指定数量的神经元，每个神经元都有一组权重
            for (int i = 0; i < neuronCount; i++)
            {
                neurons.Add(new Neuron(inputCountPerNeuron));
            }
        }

        public void FeedForward(float[] inputs)
        {
            outputs = new float[neurons.Count];

            // 计算每个神经元的输出
            for (int i = 0; i < neurons.Count; i++)
            {
                outputs[i] = neurons[i].CalculateOutput(inputs);
            }
        }
        public NeuronLayer Clone()
        {
            NeuronLayer clone = new NeuronLayer(neurons.Count, neurons[0].GetWeights().Length);

            for (int i = 0; i < neurons.Count; i++)
            {
                clone.neurons[i] = neurons[i].Clone();
            }

            return clone;
        }

        public void Mutate(float mutationRate)
        {
            foreach (Neuron neuron in neurons)
            {
                neuron.Mutate(mutationRate);
            }
        }
        public NeuronLayer Crossover(NeuronLayer other)
        {
            NeuronLayer child = new NeuronLayer(neurons.Count, neurons[0].GetWeights().Length);
            for (int i = 0; i < neurons.Count; i++)
            {
                child.neurons[i] = neurons[i].Crossover(other.neurons[i]);
            }
            return child;
        }
    }
    [System.Serializable]
    public class Neuron
    {
        private float[] weights;  // 权重数组
        public float bias;  // 偏置值

        // 用于生成随机权重和偏置的随机数生成器
        private static System.Random random = new System.Random();

        public Neuron(int inputCount)
        {
            weights = new float[inputCount];

            // 初始化权重和偏置为随机值
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = (float)random.NextDouble() * 2 - 1;  // 随机权重在-1和1之间
            }
            bias = (float)random.NextDouble() * 2 - 1;  // 随机偏置在-1和1之间
        }

        public float CalculateOutput(float[] inputs)
        {
            float output = 0;

            // 计算权重和输入的乘积之和
            for (int i = 0; i < Mathf.Min(weights.Length, inputs.Length); i++)
            {
                output += weights[i] * inputs[i];
            }


            // 添加偏置
            output += bias;

            // 应用激活函数（例如sigmoid函数）处理输出值
            output = Sigmoid(output);

            return output;
        }

        private float Sigmoid(float x)
        {
            // sigmoid函数能够将任何值转换到0和1之间，常用于二元分类问题
            return 1 / (1 + Mathf.Exp(-x));
        }

        public float[] GetWeights()
        {
            return weights;
        }

        public void SetWeights(float[] newWeights)
        {
            for (int i = 0; i < Mathf.Min(weights.Length, newWeights.Length); i++)
            {
                weights[i] = newWeights[i];
            }
        }

        public Neuron Clone()
        {
            Neuron clone = new Neuron(weights.Length);

            clone.weights = (float[])weights.Clone();
            clone.bias = bias;

            return clone;
        }

        public void Mutate(float mutationRate)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                if (random.NextDouble() < mutationRate)
                {
                    weights[i] += (float)random.NextDouble() * 2 - 1;
                }
            }

            if (random.NextDouble() < mutationRate)
            {
                bias += (float)random.NextDouble() * 2 - 1;
            }
        }
        public Neuron Crossover(Neuron other)
        {
            Neuron child = new Neuron(weights.Length);
            for (int i = 0; i < weights.Length; i++)
            {
                child.weights[i] = (weights[i] + other.weights[i]) / 2f;
            }
            child.bias = (bias + other.bias) / 2f;
            return child;
        }
    }
}
