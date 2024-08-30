using System;

namespace Assets.Source.Code_base
{
    public class EnemySpawnVisitor : IEnemyVisitor
    {
        private readonly ObjectPool<Enemy> _asteroidBigPool;
        private readonly ObjectPool<Enemy> _asteroidMiniPool;
        private readonly ObjectPool<Enemy> _ufoPool;

        private Enemy _enemy;

        public Enemy Enemy => _enemy;

        public EnemySpawnVisitor()
        {
            _asteroidBigPool = new();
            _asteroidMiniPool = new();
            _ufoPool = new();
        }

        public void Visit(Asteroid enemy) =>
            _asteroidBigPool.Put(enemy);

        public void Visit(CharacterFollower characterFollower) =>
            _ufoPool.Put(characterFollower);

        public void Visit(MiniAsteroid miniAsteroid) =>
            _asteroidMiniPool.Put(miniAsteroid);

        public bool TrySetEnemy(EnemyNames name)
        {
            switch (name)
            {
                case EnemyNames.AsteroidBig:
                    return _asteroidBigPool.TryGet(out _enemy);

                case EnemyNames.AsteroidMini:
                    return _asteroidMiniPool.TryGet(out _enemy);

                case EnemyNames.UFO:
                    return _ufoPool.TryGet(out _enemy);

                default:
                    throw new Exception("add EnemyNames");
            }
        }
    }
}
