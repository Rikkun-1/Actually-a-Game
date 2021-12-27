using System.Collections.Generic;
using System.Linq;
using Data;
using Entitas;
using Humanizer;
using RotaryHeart.Lib.AutoComplete;
using UnityEditor;
using UnityEngine;


    [CustomEditor(typeof(SystemDisablingSettings))]
    public class SystemDisablingEditor : Editor
    {
        private static SystemDisablingSettings _systemDisablingSettings;

        private static bool         _foldout;
        private static List<string> _systemsToDeactivate;
        private static List<string> _deactivatedSystems;

        private static string[] _cachedOptions;

        private void OnEnable()
        {
            _systemDisablingSettings = (SystemDisablingSettings)target;
            _deactivatedSystems      = _systemDisablingSettings.deactivatedSystems;
            _systemsToDeactivate     = _systemDisablingSettings.systemsToDeactivate;
            _foldout                 = _systemDisablingSettings.foldout;

            _cachedOptions = GameUtils.GetInterfaceImplementers<ISystem>()
                                      .Select(option => option.EndsWith("Systems")
                                                            ? option.Humanize(LetterCasing.Title)
                                                            : option.RemoveSystemSuffix())
                                      .ToArray();
        }

        private void OnDisable()
        {
            _systemDisablingSettings.foldout = _foldout;
        }

        public override void OnInspectorGUI()
        {
            _foldout = EditorGUILayout.Foldout(_foldout, "System disabling");
            if (_foldout)
            {
                EditorGUI.indentLevel++;
                for (var i = 0; i < _systemsToDeactivate.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    DrawSystemWithStatus(i);

                    if (GUILayout.Button("X", GUILayout.Width(20)))
                    {
                        DeleteSystem(i);
                    }

                    EditorGUILayout.EndHorizontal();
                }

                DrawAddNewSystem();
                SaveSettings();
                EditorGUI.indentLevel--;
            }
        }

        private static void DrawSystemWithStatus(int index)
        {
            var status    = !_deactivatedSystems.Contains(_systemsToDeactivate[index]);
            var newStatus = EditorGUILayout.ToggleLeft(_systemsToDeactivate[index], status);
            if (newStatus != status)
            {
                if (newStatus)
                {
                    _deactivatedSystems.Remove(_systemsToDeactivate[index]);
                }
                else
                {
                    _deactivatedSystems.AddIfNotPresented(_systemsToDeactivate[index]);
                }
            }
        }

        private static void DeleteSystem(int index)
        {
            _deactivatedSystems.Remove(_systemsToDeactivate[index]);
            _systemsToDeactivate.Remove(_systemsToDeactivate[index]);
        }

        private static void SaveSettings()
        {
            _systemDisablingSettings.systemsToDeactivate = _systemsToDeactivate;
            _systemDisablingSettings.deactivatedSystems  = _deactivatedSystems;
        }

        private static void DrawAddNewSystem()
        {
            var systemToAdd =
                AutoCompleteTextField.EditorGUILayout.AutoCompleteTextField("Add system", "", _cachedOptions);

            if (systemToAdd != "")
            {
                _systemsToDeactivate.AddIfNotPresented(systemToAdd);
                _deactivatedSystems.AddIfNotPresented(systemToAdd);
            }
        }
    }
